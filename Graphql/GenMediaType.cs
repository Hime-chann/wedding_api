using HotChocolate;
using wedding_api.Models;

namespace wedding_api.GraphQL.Types
{
    public class GenMediaType : ObjectType<GeneralMediaUploading>
    {
        protected override void Configure(IObjectTypeDescriptor<GeneralMediaUploading> descriptor)
        {
            descriptor.Field(m => m.ContentUrl).Type<StringType>();
            descriptor.Field(m => m.CreatedAt).Type<DateTimeType>();
        }
    }
}
