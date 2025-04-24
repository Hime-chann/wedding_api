using HotChocolate;
using wedding_api.Models;

namespace wedding_api.GraphQL.Types
{
    public class WeddingProfileType : ObjectType<WeddingProfile>
    {
        protected override void Configure(IObjectTypeDescriptor<WeddingProfile> descriptor)
        {
            descriptor.Field(w => w.EventTitle).Type<StringType>();
            descriptor.Field(w => w.BrideName).Type<StringType>();
            descriptor.Field(w => w.GroomName).Type<StringType>();
            descriptor.Field(w => w.WeddingDate).Type<DateTimeType>();
            descriptor.Field(w => w.EventPictureUrl).Type<StringType>();
            descriptor.Field(w => w.BackgroundPictureUrl).Type<StringType>();
            descriptor.Field(w => w.Bio).Type<StringType>();
            descriptor.Field(w => w.QrCodeHash).Type<StringType>();
            descriptor.Field(w => w.CreatedAt).Type<DateTimeType>();
        }
    }
}
