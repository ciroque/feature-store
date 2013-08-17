﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ciroque.Foundations.FeatureStore.HealthChecks {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MessageResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MessageResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Ciroque.Foundations.FeatureStore.HealthChecks.MessageResources", typeof(MessageResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The IsHealthy_ObjectExistenceCheck sproc produced no result row. .
        /// </summary>
        internal static string EXISTENCE_SPROC_RETURNED_NO_ROWS {
            get {
                return ResourceManager.GetString("EXISTENCE_SPROC_RETURNED_NO_ROWS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Feature Store service is not running..
        /// </summary>
        internal static string FEATURE_STORE_SERVICE_IS_NOT_RUNNING {
            get {
                return ResourceManager.GetString("FEATURE_STORE_SERVICE_IS_NOT_RUNNING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Feature Store service is running..
        /// </summary>
        internal static string FEATURE_STORE_SERVICE_IS_RUNNING {
            get {
                return ResourceManager.GetString("FEATURE_STORE_SERVICE_IS_RUNNING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The call to check if the user was in the necessary role returned no row..
        /// </summary>
        internal static string IS_ROLEMEMBER_CHECK_RETURNED_NO_RECORDS {
            get {
                return ResourceManager.GetString("IS_ROLEMEMBER_CHECK_RETURNED_NO_RECORDS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The log4net configuration is correct..
        /// </summary>
        internal static string LOG4NET_CONFIGURATION_IS_CORRECT {
            get {
                return ResourceManager.GetString("LOG4NET_CONFIGURATION_IS_CORRECT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Found &apos;{0}&apos; object in database..
        /// </summary>
        internal static string OBJECT_FOUND_MESSAGE_FORMAT {
            get {
                return ResourceManager.GetString("OBJECT_FOUND_MESSAGE_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to find &apos;{0}&apos; object in database..
        /// </summary>
        internal static string OBJECT_NOT_FOUND_MESSAGE_FORMAT {
            get {
                return ResourceManager.GetString("OBJECT_NOT_FOUND_MESSAGE_FORMAT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The performance counter category and all counters have been registered..
        /// </summary>
        internal static string PERFORMANCE_CATEGORY_AND_COUNTERS_DEFINED {
            get {
                return ResourceManager.GetString("PERFORMANCE_CATEGORY_AND_COUNTERS_DEFINED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The performance counter category has been registered, but counter(s) are missing. Missing counters: {0}.
        /// </summary>
        internal static string PERFORMANCE_CATEGORY_DEFINED_MISSING_COUNTERS {
            get {
                return ResourceManager.GetString("PERFORMANCE_CATEGORY_DEFINED_MISSING_COUNTERS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The performance counter category has not been registered..
        /// </summary>
        internal static string PERFORMANCE_CATEGORY_NOT_DEFINED {
            get {
                return ResourceManager.GetString("PERFORMANCE_CATEGORY_NOT_DEFINED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connection to SqlServer database succeeded. Connection string: {0}.
        /// </summary>
        internal static string SQL_CONNECTION_OPEN_SUCCEEDED {
            get {
                return ResourceManager.GetString("SQL_CONNECTION_OPEN_SUCCEEDED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user was found in the necessary role..
        /// </summary>
        internal static string USER_FOUND_IN_ROLE {
            get {
                return ResourceManager.GetString("USER_FOUND_IN_ROLE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user was not found in the necessary role..
        /// </summary>
        internal static string USER_NOT_FOUND_IN_ROLE {
            get {
                return ResourceManager.GetString("USER_NOT_FOUND_IN_ROLE", resourceCulture);
            }
        }
    }
}