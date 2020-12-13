﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace PaderConference.Core.Services.Permissions
{
    /// <summary>
    ///     Aggregate all permissions defined in <see cref="PermissionsList" /> in an accessible collection
    /// </summary>
    public static class PermissionsListUtil
    {
        static PermissionsListUtil()
        {
            var result = new Dictionary<string, PermissionDescriptor>();

            var permissionClasses = typeof(PermissionsList).GetNestedTypes().Concat(new[] {typeof(PermissionsList)});
            foreach (var permissionClass in permissionClasses)
            foreach (var fieldInfo in permissionClass.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (typeof(PermissionDescriptor).IsAssignableFrom(fieldInfo.FieldType))
                {
                    var descriptor = (PermissionDescriptor?) fieldInfo.GetValue(null);
                    if (descriptor != null)
                        result.Add(descriptor.Key, descriptor);
                }
            }

            All = result.ToImmutableDictionary();
        }

        /// <summary>
        ///     All available permissions defined in <see cref="PermissionsList" />
        /// </summary>
        public static IImmutableDictionary<string, PermissionDescriptor> All { get; }
    }
}
