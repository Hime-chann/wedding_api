//signupmutation.cs


using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using wedding_api.DTOs;
using wedding_api.Models;
using wedding_api.Services.AdminServices;


namespace wedding_api.GraphQL;

[ExtendObjectType("Mutation")]
public class SignupMutation
{
    public async Task<Admin> Signup(
        AdminSignupDTO input,
        [Service] WedDbContext db,
        [Service] ValidemailService emailService,
        [Service] PasswordHashService passwordService)
    {
        // Validate Email Format
        if (!emailService.IsValidEmail(input.Email))
        {
            // This will return an error with a custom message in the response
            throw new GraphQLException(new Error("Invalid email format."));
        }

        // Check if Email Already Exists
        if (emailService.IsEmailTaken(input.Email))
        {
            throw new GraphQLException(new Error("Email is already in use."));
        }

        // Hash Password
        string hashedPassword = passwordService.HashPassword(input.Password);

        // Create New User
        var admin = new Admin
        {
            Email = input.Email,
            PasswordHash = passwordService.HashPassword(input.Password),
        };

        db.Admins.Add(admin);
        await db.SaveChangesAsync();
        return admin; // Return the created user object
    }


}
