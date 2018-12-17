using System.Collections.Generic;
using System.Reflection;

namespace Ultrix.Application.Converters
{
    public static class EntityToDtoConverter
    {
        public static TDto Convert<TDto, TEntity>(TEntity entity) where TDto : class, new()
        {
            TDto dto = new TDto();
            IList<PropertyInfo> dtoProperties = new List<PropertyInfo>(typeof(TDto).GetProperties());
            foreach (PropertyInfo property in dtoProperties)
            {
                object value = entity.GetType().GetProperty(property.Name).GetValue(entity);
                property.SetValue(dto, value);
            }
            return dto;
        }
    }
}
