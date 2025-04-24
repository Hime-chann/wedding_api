using HotChocolate;
using wedding_api.Models;

namespace wedding_api.GraphQL.Types
{
    public class StoryReactionType : ObjectType<StoryReaction>
    {
        protected override void Configure(IObjectTypeDescriptor<StoryReaction> descriptor)
        {
            descriptor.Field(r => r.ReactionType).Type<StringType>();
            descriptor.Field(r => r.SessionHash).Type<StringType>();
            descriptor.Field(r => r.CreatedAt).Type<DateTimeType>();
        }
    }
}