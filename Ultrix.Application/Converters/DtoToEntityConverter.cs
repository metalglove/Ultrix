using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ultrix.Application.Converters
{
    public static class DtoToEntityConverter
    {
        public static TEntity Convert<TEntity, TDto>(TDto dto) where TEntity : class, new()
        {
            TEntity entity = new TEntity();
            IList<PropertyInfo> dtoProperties = new List<PropertyInfo>(typeof(TDto).GetProperties());
            IList<PropertyInfo> entityProperties = new List<PropertyInfo>(typeof(TEntity).GetProperties());
            IList<PropertyInfo> joinedProperties = 
                dtoProperties.Join(entityProperties, 
                dtoProperty => dtoProperty.Name, 
                entityProperty => entityProperty.Name, 
                (dtoProperty, entityProperty) => entityProperty)
                .ToList();
            foreach (PropertyInfo property in joinedProperties)
            {
                object value = dto.GetType().GetProperty(property.Name).GetValue(dto);
                if (value == null) continue;
                property.SetValue(entity, value);
            }
            return entity;
        }
    }
}
