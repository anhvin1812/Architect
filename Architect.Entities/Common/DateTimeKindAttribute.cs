using System;
using System.Linq;
using System.Reflection;

namespace Architect.Entities.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeKindAttribute : Attribute
    {
        private readonly DateTimeKind _kind;

        public DateTimeKindAttribute(DateTimeKind kind)
        {
            _kind = kind;
        }

        public DateTimeKind Kind
        {
            get { return _kind; }
        }

        public static void Apply(object entity)
        {
            // Allow only Entities inherits from CleanEntityBase
            if (entity == null || !typeof (CleanEntityBase).IsAssignableFrom(entity.GetType()))
                return;

            var properties = entity.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof (DateTime) || p.PropertyType == typeof (DateTime?));

            foreach (var property in properties)
            {
                var dt = (property.PropertyType == typeof (DateTime?))
                    ? (DateTime?) property.GetValue(entity)
                    : (DateTime) property.GetValue(entity);
                if (dt == null)
                    continue;

                var attr = property.GetCustomAttribute<DateTimeKindAttribute>();

                // Set DateTimeKind to UTC by default, unless the DateTime is decorated with DateTimeKindAttribute
                property.SetValue(entity,
                    attr == null
                        ? DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc)
                        : DateTime.SpecifyKind(dt.Value, attr.Kind));
            }
        }
    }
}
