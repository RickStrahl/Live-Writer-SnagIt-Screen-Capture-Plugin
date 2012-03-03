using System;
using System.Reflection;

using System.Globalization;
using System.Collections;

namespace Westwind.Tools
{



    /// <summary>
    /// wwUtils class which contains a set of common utility classes for 
    /// Formatting strings
    /// Reflection Helpers
    /// Object Serialization
    /// </summary>
    public class ReflectionUtils
    {

                /// <summary>
        /// Binding Flags constant to be reused for all Reflection access methods.
        /// </summary>
        public const BindingFlags MemberAccess =
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase;


        /// <summary>
        /// Retrieve a property value from an object dynamically. This is a simple version
        /// that uses Reflection calls directly. It doesn't support indexers.
        /// </summary>
        /// <param name="Object">Object to make the call on</param>
        /// <param name="Property">Property to retrieve</param>
        /// <returns>Object - cast to proper type</returns>
        public static object GetProperty(object Object, string Property)
        {
            return Object.GetType().GetProperty(Property, ReflectionUtils.MemberAccess).GetValue(Object, null);
        }

        /// <summary>
        /// Retrieve a field dynamically from an object. This is a simple implementation that's
        /// straight Reflection and doesn't support indexers.
        /// </summary>
        /// <param name="Object">Object to retreve Field from</param>
        /// <param name="Property">name of the field to retrieve</param>
        /// <returns></returns>
        public static object GetField(object Object, string Property)
        {
            return Object.GetType().GetField(Property, ReflectionUtils.MemberAccess).GetValue(Object);
        }

        /// <summary>
        /// Parses Properties and Fields including Array and Collection references.
        /// Used internally for the 'Ex' Reflection methods.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Property"></param>
        /// <returns></returns>
        private static object GetPropertyInternal(object Parent, string Property)
        {
            if (Property == "this" || Property == "me")
                return Parent;

            object Result = null;
            string PureProperty = Property;
            string Indexes = null;
            bool IsArrayOrCollection = false;

            // *** Deal with Array Property
            if (Property.IndexOf("[") > -1)
            {
                PureProperty = Property.Substring(0, Property.IndexOf("["));
                Indexes = Property.Substring(Property.IndexOf("["));
                IsArrayOrCollection = true;
            }

            // *** Get the member
            MemberInfo Member = Parent.GetType().GetMember(PureProperty, ReflectionUtils.MemberAccess)[0];
            if (Member.MemberType == MemberTypes.Property)
                Result = ((PropertyInfo)Member).GetValue(Parent, null);
            else
                Result = ((FieldInfo)Member).GetValue(Parent);

            if (IsArrayOrCollection)
            {
                Indexes = Indexes.Replace("[", "").Replace("]", "");

                if (Result is Array)
                {
                    int Index = -1;
                    int.TryParse(Indexes, out Index);
                    Result = CallMethod(Result, "GetValue", Index);
                }
                else if (Result is ICollection)
                {
                    if (Indexes.StartsWith("\""))
                    {
                        // *** String Index
                        Indexes = Indexes.Trim('\"');
                        Result = CallMethod(Result, "get_Item", Indexes);
                    }
                    else
                    {
                        // *** assume numeric index
                        int Index = -1;
                        int.TryParse(Indexes, out Index);
                        Result = CallMethod(Result, "get_Item", Index);
                    }
                }

            }

            return Result;
        }

        /// <summary>
        /// Parses Properties and Fields including Array and Collection references.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Property"></param>
        /// <returns></returns>
        private static object SetPropertyInternal(object Parent, string Property, object Value)
        {
            if (Property == "this" || Property == "me")
                return Parent;

            object Result = null;
            string PureProperty = Property;
            string Indexes = null;
            bool IsArrayOrCollection = false;

            // *** Deal with Array Property
            if (Property.IndexOf("[") > -1)
            {
                PureProperty = Property.Substring(0, Property.IndexOf("["));
                Indexes = Property.Substring(Property.IndexOf("["));
                IsArrayOrCollection = true;
            }

            if (!IsArrayOrCollection)
            {
                // *** Get the member
                MemberInfo Member = Parent.GetType().GetMember(PureProperty, ReflectionUtils.MemberAccess)[0];
                if (Member.MemberType == MemberTypes.Property)
                    ((PropertyInfo)Member).SetValue(Parent, Value, null);
                else
                    ((FieldInfo)Member).SetValue(Parent, Value);
                return null;
            }
            else
            {
                // *** Get the member
                MemberInfo Member = Parent.GetType().GetMember(PureProperty, ReflectionUtils.MemberAccess)[0];
                if (Member.MemberType == MemberTypes.Property)
                    Result = ((PropertyInfo)Member).GetValue(Parent, null);
                else
                    Result = ((FieldInfo)Member).GetValue(Parent);
            }
            if (IsArrayOrCollection)
            {
                Indexes = Indexes.Replace("[", "").Replace("]", "");

                if (Result is Array)
                {
                    int Index = -1;
                    int.TryParse(Indexes, out Index);
                    Result = CallMethod(Result, "SetValue", Value, Index);
                }
                else if (Result is ICollection)
                {
                    if (Indexes.StartsWith("\""))
                    {
                        // *** String Index
                        Indexes = Indexes.Trim('\"');
                        Result = CallMethod(Result, "set_Item", Indexes, Value);
                    }
                    else
                    {
                        // *** assume numeric index
                        int Index = -1;
                        int.TryParse(Indexes, out Index);
                        Result = CallMethod(Result, "set_Item", Index, Value);
                    }
                }

            }

            return Result;
        }

        /// <summary>
        /// Returns a property or field value using a base object and sub members including . syntax.
        /// For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
        /// This method also supports indexers in the Property value such as:
        /// Customer.DataSet.Tables["Customers"].Rows[0]
        /// </summary>
        /// <param name="Parent">Parent object to 'start' parsing from. Typically this will be the Page.</param>
        /// <param name="Property">The property to retrieve. Example: 'Customer.Entity.Company'</param>
        /// <returns></returns>
        public static object GetPropertyEx(object Parent, string Property)
        {
            Type Type = Parent.GetType();

            int lnAt = Property.IndexOf(".");
            if (lnAt < 0)
            {
                // *** Complex parse of the property    
                return GetPropertyInternal(Parent, Property);
            }

            // *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
            string Main = Property.Substring(0, lnAt);
            string Subs = Property.Substring(lnAt + 1);

            // *** Retrieve the next . section of the property
            object Sub = GetPropertyInternal(Parent, Main);

            // *** Now go parse the left over sections
            return GetPropertyEx(Sub, Subs);
        }

        /// <summary>
        /// Sets the property on an object. This is a simple method that uses straight Reflection 
        /// and doesn't support indexers.
        /// </summary>
        /// <param name="Object">Object to set property on</param>
        /// <param name="Property">Name of the property to set</param>
        /// <param name="Value">value to set it to</param>
        public static void SetProperty(object Object, string Property, object Value)
        {
            Object.GetType().GetProperty(Property, ReflectionUtils.MemberAccess).SetValue(Object, Value, null);
        }

        /// <summary>
        /// Sets the field on an object. This is a simple method that uses straight Reflection 
        /// and doesn't support indexers.
        /// </summary>
        /// <param name="Object">Object to set property on</param>
        /// <param name="Property">Name of the field to set</param>
        /// <param name="Value">value to set it to</param>
        public static void SetField(object Object, string Property, object Value)
        {
            Object.GetType().GetField(Property, ReflectionUtils.MemberAccess).SetValue(Object, Value);
        }

        /// <summary>
        /// Sets a value on an object. Supports . syntax for named properties
        /// (ie. Customer.Entity.Company) as well as indexers.
        /// </summary>
        /// <param name="Object Parent">
        /// Object to set the property on.
        /// </param>
        /// <param name="String Property">
        /// Property to set. Can be an object hierarchy with . syntax and can 
        /// include indexers. Examples: Customer.Entity.Company, 
        /// Customer.DataSet.Tables["Customers"].Rows[0]
        /// </param>
        /// <param name="Object Value">
        /// Value to set the property to
        /// </param>
        public static object SetPropertyEx(object Parent, string Property, object Value)
        {
            Type Type = Parent.GetType();

            // *** no more .s - we got our final object
            int lnAt = Property.IndexOf(".");
            if (lnAt < 0)
            {
                SetPropertyInternal(Parent, Property, Value);
                return null;
            }

            // *** Walk the . syntax
            string Main = Property.Substring(0, lnAt);
            string Subs = Property.Substring(lnAt + 1);

            object Sub = GetPropertyInternal(Parent, Main);

            SetPropertyEx(Sub, Subs, Value);

            return null;
        }

        /// <summary>
        /// Calls a method on an object dynamically.
        /// </summary>
        /// <param name="Params"></param>
        /// 1st - Method name, 2nd - 1st parameter, 3rd - 2nd parm etc.
        /// <returns></returns>
        public static object CallMethod(object Object, string Method, params object[] Params)
        {
            // *** Pick up parameter types so we can match the method properly
            Type[] ParmTypes = null;
            if (Params != null)
            {
                ParmTypes = new Type[Params.Length];
                for (int x = 0; x < Params.Length; x++)
                {
                    ParmTypes[x] = Params[x].GetType();
                }
            }

            return Object.GetType().GetMethod(Method, ReflectionUtils.MemberAccess | BindingFlags.InvokeMethod, null, ParmTypes, null).Invoke(Object, Params);

            // *** More reliable but slower
            //return Object.GetType().InvokeMember(Method,ReflectionUtils.MemberAccess | BindingFlags.InvokeMethod,null,Object,Params);

            //return Object.GetType().GetMethod(Method,ReflectionUtils.MemberAccess | BindingFlags.InvokeMethod).Invoke(Object,Params);
        }


        /// <summary>
        /// Calls a method on an object with extended . syntax (object: this Method: Entity.CalculateOrderTotal)
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Method"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public static object CallMethodEx(object Parent, string Method, params object[] Params)
        {
            Type Type = Parent.GetType();

            // *** no more .s - we got our final object
            int lnAt = Method.IndexOf(".");
            if (lnAt < 0)
            {
                return ReflectionUtils.CallMethod(Parent, Method, Params);
            }

            // *** Walk the . syntax
            string Main = Method.Substring(0, lnAt);
            string Subs = Method.Substring(lnAt + 1);

            object Sub = GetPropertyInternal(Parent, Main);

            // *** Recurse until we get the lowest ref
            return CallMethodEx(Sub, Subs, Params);
        }


        /// <summary>
        /// Creates an instance from a type by calling the parameterless constructor.
        /// 
        /// Note this will not work with COM objects - continue to use the Activator.CreateInstance
        /// for COM objects.
        /// <seealso>Class wwUtils</seealso>
        /// </summary>
        /// <param name="TypeToCreate">
        /// The type from which to create an instance.
        /// </param>
        /// <returns>object</returns>
        public object CreateInstanceFromType(Type TypeToCreate)
        {
            Type[] Parms = Type.EmptyTypes;
            return TypeToCreate.GetConstructor(Parms).Invoke(null);
        }

        /// <summary>
        /// Converts a type to string if possible. This method supports an optional culture generically on any value.
        /// It calls the ToString() method on common types and uses a type converter on all other objects
        /// if available
        /// </summary>
        /// <param name="RawValue">The Value or Object to convert to a string</param>
        /// <param name="Culture">Culture for numeric and DateTime values</param>
        /// <returns>string</returns>
        public static string TypedValueToString(object RawValue, CultureInfo Culture)
        {
            Type ValueType = RawValue.GetType();
            string Return = null;

            if (ValueType == typeof(string))
                Return = RawValue.ToString();
            else if (ValueType == typeof(int) || ValueType == typeof(decimal) ||
                ValueType == typeof(double) || ValueType == typeof(float))
                Return = string.Format(Culture.NumberFormat, "{0}", RawValue);
            else if (ValueType == typeof(DateTime))
                Return = string.Format(Culture.DateTimeFormat, "{0}", RawValue);
            else if (ValueType == typeof(bool))
                Return = RawValue.ToString();
            else if (ValueType == typeof(byte))
                Return = RawValue.ToString();
            else if (ValueType.IsEnum)
                Return = RawValue.ToString();
            else
            {
                // Any type that supports a type converter
                System.ComponentModel.TypeConverter converter =
                    System.ComponentModel.TypeDescriptor.GetConverter(ValueType);
                if (converter != null && converter.CanConvertTo(typeof(string)))
                    Return = converter.ConvertToString(null, Culture, RawValue);
                else
                    // Last resort - just call ToString() on unknown type
                    Return = RawValue.ToString();

            }

            return Return;
        }

        /// <summary>
        /// Converts a type to string if possible. This method uses the current culture for numeric and DateTime values.
        /// It calls the ToString() method on common types and uses a type converter on all other objects
        /// if available.
        /// </summary>
        /// <param name="RawValue">The Value or Object to convert to a string</param>
        /// <param name="Culture">Culture for numeric and DateTime values</param>
        /// <returns>string</returns>
        public static string TypedValueToString(object RawValue)
        {
            return TypedValueToString(RawValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Turns a string into a typed value. Useful for auto-conversion routines
        /// like form variable or XML parsers.
        /// <seealso>Class wwUtils</seealso>
        /// </summary>
        /// <param name="SourceString">
        /// The string to convert from
        /// </param>
        /// <param name="TargetType">
        /// The type to convert to
        /// </param>
        /// <param name="Culture">
        /// Culture used for numeric and datetime values.
        /// </param>
        /// <returns>object. Throws exception if it cannot be converted.</returns>
        public static object StringToTypedValue(string SourceString, Type TargetType, CultureInfo Culture)
        {
            object Result = null;

            if (TargetType == typeof(string))
                Result = SourceString;
            else if (TargetType == typeof(int))
                Result = int.Parse(SourceString, NumberStyles.Integer, Culture.NumberFormat);
            else if (TargetType == typeof(byte))
                Result = Convert.ToByte(SourceString);
            else if (TargetType == typeof(decimal))
                Result = Decimal.Parse(SourceString, NumberStyles.Any, Culture.NumberFormat);
            else if (TargetType == typeof(double))
                Result = Double.Parse(SourceString, NumberStyles.Any, Culture.NumberFormat);
            else if (TargetType == typeof(bool))
            {
                if (SourceString.ToLower() == "true" || SourceString.ToLower() == "on" || SourceString == "1")
                    Result = true;
                else
                    Result = false;
            }
            else if (TargetType == typeof(DateTime))
                Result = Convert.ToDateTime(SourceString, Culture.DateTimeFormat);
            else if (TargetType.IsEnum)
                Result = Enum.Parse(TargetType, SourceString);
            else
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(TargetType);
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                    Result = converter.ConvertFromString(null, Culture, SourceString);
                else
                {
                    System.Diagnostics.Debug.Assert(false, "Type Conversion not handled in StringToTypedValue for " +
                                                    TargetType.Name + " " + SourceString);
                    throw (new ApplicationException("Type Conversion not handled in StringToTypedValue"));
                }
            }

            return Result;
        }

        /// <summary>
        /// Turns a string into a typed value. Useful for auto-conversion routines
        /// like form variable or XML parsers.
        /// </summary>
        /// <param name="SourceString">The input string to convert</param>
        /// <param name="TargetType">The Type to convert it to</param>
        /// <returns>object reference. Throws Exception if type can not be converted</returns>
        public static object StringToTypedValue(string SourceString, Type TargetType)
        {
            return StringToTypedValue(SourceString, TargetType, CultureInfo.CurrentCulture);
        }

    
        /// <summary>
        /// Retrieve a dynamic 'non-typelib' property
        /// </summary>
        /// <param name="Object">Object to make the call on</param>
        /// <param name="Property">Property to retrieve</param>
        /// <returns></returns>
        public static object GetPropertyCom(object Object, string Property)
        {
            return Object.GetType().InvokeMember(Property, ReflectionUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField, null,
                                                Object, null);
        }


        /// <summary>
        /// Returns a property or field value using a base object and sub members including . syntax.
        /// For example, you can access: this.oCustomer.oData.Company with (this,"oCustomer.oData.Company")
        /// </summary>
        /// <param name="Parent">Parent object to 'start' parsing from.</param>
        /// <param name="Property">The property to retrieve. Example: 'oBus.oData.Company'</param>
        /// <returns></returns>
        public static object GetPropertyExCom(object Parent, string Property)
        {

            Type Type = Parent.GetType();

            int lnAt = Property.IndexOf(".");
            if (lnAt < 0)
            {
                if (Property == "this" || Property == "me")
                    return Parent;

                // *** Get the member
                return Parent.GetType().InvokeMember(Property, ReflectionUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField, null,
                    Parent, null);
            }

            // *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
            string Main = Property.Substring(0, lnAt);
            string Subs = Property.Substring(lnAt + 1);

            object Sub = Parent.GetType().InvokeMember(Main, ReflectionUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField, null,
                Parent, null);

            // *** Recurse further into the sub-properties (Subs)
            return ReflectionUtils.GetPropertyExCom(Sub, Subs);
        }

        /// <summary>
        /// Sets the property on an object.
        /// </summary>
        /// <param name="Object">Object to set property on</param>
        /// <param name="Property">Name of the property to set</param>
        /// <param name="Value">value to set it to</param>
        public static void SetPropertyCom(object Object, string Property, object Value)
        {
            Object.GetType().InvokeMember(Property, ReflectionUtils.MemberAccess | BindingFlags.SetProperty | BindingFlags.SetField, null, Object, new object[1] { Value });
            //GetProperty(Property,ReflectionUtils.MemberAccess).SetValue(Object,Value,null);
        }

        /// <summary>
        /// Sets the value of a field or property via Reflection. This method alws 
        /// for using '.' syntax to specify objects multiple levels down.
        /// 
        /// ReflectionUtils.SetPropertyEx(this,"Invoice.LineItemsCount",10)
        /// 
        /// which would be equivalent of:
        /// 
        /// this.Invoice.LineItemsCount = 10;
        /// </summary>
        /// <param name="Object Parent">
        /// Object to set the property on.
        /// </param>
        /// <param name="String Property">
        /// Property to set. Can be an object hierarchy with . syntax.
        /// </param>
        /// <param name="Object Value">
        /// Value to set the property to
        /// </param>
        public static object SetPropertyExCom(object Parent, string Property, object Value)
        {
            Type Type = Parent.GetType();

            int lnAt = Property.IndexOf(".");
            if (lnAt < 0)
            {
                // *** Set the member
                Parent.GetType().InvokeMember(Property, ReflectionUtils.MemberAccess | BindingFlags.SetProperty | BindingFlags.SetField, null,
                    Parent, new object[1] { Value });

                return null;
            }

            // *** Walk the . syntax - split into current object (Main) and further parsed objects (Subs)
            string Main = Property.Substring(0, lnAt);
            string Subs = Property.Substring(lnAt + 1);


            object Sub = Parent.GetType().InvokeMember(Main, ReflectionUtils.MemberAccess | BindingFlags.GetProperty | BindingFlags.GetField, null,
                Parent, null);

            return SetPropertyExCom(Sub, Subs, Value);
        }


        /// <summary>
        /// Wrapper method to call a 'dynamic' (non-typelib) method
        /// on a COM object
        /// </summary>
        /// <param name="Params"></param>
        /// 1st - Method name, 2nd - 1st parameter, 3rd - 2nd parm etc.
        /// <returns></returns>
        public static object CallMethodCom(object Object, string Method, params object[] Params)
        {
            return Object.GetType().InvokeMember(Method, ReflectionUtils.MemberAccess | BindingFlags.InvokeMethod, null, Object, Params);
        }

    }
}





