using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Ultrix.Persistance.Contexts;

namespace Ultrix.Persistance.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            MethodInfo applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            _ = typeof(ApplicationDbContext).Assembly
                .GetTypes()
                .Select(t => (t, i: t.GetInterfaces().FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();
        }
    }
}
