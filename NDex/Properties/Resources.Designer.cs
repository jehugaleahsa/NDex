﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NDex.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NDex.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to An attempt was made to aggregate the values in an empty list without providing a seed..
        /// </summary>
        internal static string AggregateEmptyList {
            get {
                return ResourceManager.GetString("AggregateEmptyList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given array was too small to hold the items..
        /// </summary>
        internal static string ArrayTooSmall {
            get {
                return ResourceManager.GetString("ArrayTooSmall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given count was negative or too large..
        /// </summary>
        internal static string CountOutOfRange {
            get {
                return ResourceManager.GetString("CountOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An attempt was made to edit a read-only list..
        /// </summary>
        internal static string EditReadonlyList {
            get {
                return ResourceManager.GetString("EditReadonlyList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The index is negative or outside the bounds of the collection..
        /// </summary>
        internal static string IndexOutOfRange {
            get {
                return ResourceManager.GetString("IndexOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The underlying collection has changed since the enumeration started..
        /// </summary>
        internal static string ListChanged {
            get {
                return ResourceManager.GetString("ListChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The given value cannot be less than {0}..
        /// </summary>
        internal static string TooSmall {
            get {
                return ResourceManager.GetString("TooSmall", resourceCulture);
            }
        }
    }
}
