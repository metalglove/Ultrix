using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Ultrix.Domain.Entities;

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
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type dtoItemType = propertyType.GetGenericArguments()[0];
                    IList genericListInstance = CreateGenericListOfType(dtoItemType);
                    dynamic list = typeof(TEntity).GetProperty(property.Name).GetValue(entity);
                    foreach (object item in list)
                    {
                        Type actualType = ((IProxyTargetAccessor)item).DynProxyGetTarget().GetType().BaseType;
                        genericListInstance.Add(Convert(dtoItemType, actualType, item));
                    }
                    property.SetValue(dto, genericListInstance);
                    continue;
                }
                object value = entity.GetType().GetProperty(property.Name).GetValue(entity);
                property.SetValue(dto, value);
            }
            return dto;
        }

        private static IList CreateGenericListOfType(Type itemType)
        {
            Type listGenericType = typeof(List<>);
            dynamic listOfItemType = listGenericType.MakeGenericType(itemType);
            return (IList)Activator.CreateInstance(listOfItemType);
        }

        private static object Convert(Type dtoItemType, Type entityType, object entity)
        {
            dynamic dto = Activator.CreateInstance(dtoItemType);
            IList<PropertyInfo> dtoProperties = new List<PropertyInfo>(dtoItemType.GetProperties());
            foreach (PropertyInfo property in dtoProperties)
            {
                Type type = property.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type newDtoItemType = type.GetGenericArguments()[0];
                    IList genericListInstance = CreateGenericListOfType(newDtoItemType);
                    dynamic list = entityType.GetProperty(property.Name).GetValue(entity);
                    foreach (dynamic item in list)
                    {
                        Type actualType = ((IProxyTargetAccessor)item).DynProxyGetTarget().GetType();
                        genericListInstance.Add(Convert(newDtoItemType, actualType, item));
                    }
                    property.SetValue(dto, genericListInstance);
                    continue;
                }

                // TODO: also check if the property name is not equal to a Entity by chance... so maybe try to check for standard types?
                if (typeof(DtoToEntityConverter).Assembly.GetTypes().Any(t => t.Name.Equals(property.Name + "Dto")))
                {
                    Type newEntityType = typeof(ApplicationUser).Assembly.GetTypes().First(t => t.Name.Equals(property.Name.Replace("Dto", "")));
                    object entityObject = entityType.GetProperty(property.Name).GetValue(entity, null);
                    object result = Convert(property.PropertyType, newEntityType, entityObject);
                    property.SetValue(dto, result);
                    continue;
                }
                object value = entityType.GetProperty(property.Name).GetValue(entity);
                property.SetValue(dto, value);
            }

            return dto;
        }
    }
}
