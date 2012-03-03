using System;
using System.IO;
using System.Text;
using System.Reflection;

using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Westwind.Tools
{

    // Serialization specific code

    public class SerializationUtils
    {

        /// <summary>
        /// Returns a string of all the field value pairs of a given object.
        /// Works only on non-statics.
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public static string ObjectToString(object Obj, string Separator, ObjectToStringTypes Type)
        {
            FieldInfo[] fi = Obj.GetType().GetFields();

            string lcOutput = "";

            if (Type == ObjectToStringTypes.Properties || Type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (PropertyInfo Property in Obj.GetType().GetProperties())
                {
                    try
                    {
                        lcOutput = lcOutput + Property.Name + ":" + Property.GetValue(Obj, null).ToString() + Separator;
                    }
                    catch
                    {
                        lcOutput = lcOutput + Property.Name + ": n/a" + Separator;
                    }
                }
            }

            if (Type == ObjectToStringTypes.Fields || Type == ObjectToStringTypes.PropertiesAndFields)
            {
                foreach (FieldInfo Field in fi)
                {
                    try
                    {
                        lcOutput = lcOutput + Field.Name + ": " + Field.GetValue(Obj).ToString() + Separator;
                    }
                    catch
                    {
                        lcOutput = lcOutput + Field.Name + ": n/a" + Separator;
                    }
                }
            }
            return lcOutput;
        }

        public enum ObjectToStringTypes
        {
            Properties,
            PropertiesAndFields,
            Fields
        }

        /// <summary>
        /// Serializes an object instance to a file.
        /// </summary>
        /// <param name="Instance">the object instance to serialize</param>
        /// <param name="Filename"></param>
        /// <param name="BinarySerialization">determines whether XML serialization or binary serialization is used</param>
        /// <returns></returns>
        public static bool SerializeObject(object Instance, string Filename, bool BinarySerialization)
        {
            bool retVal = true;

            if (!BinarySerialization)
            {
                XmlTextWriter writer = null;
                try
                {
                    XmlSerializer serializer =
                        new XmlSerializer(Instance.GetType());

                    // Create an XmlTextWriter using a FileStream.
                    Stream fs = new FileStream(Filename, FileMode.Create);
                    writer = new XmlTextWriter(fs, new UTF8Encoding());
                    writer.Formatting = Formatting.Indented;
                    writer.IndentChar = ' ';
                    writer.Indentation = 3;

                    // Serialize using the XmlTextWriter.
                    serializer.Serialize(writer, Instance);
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
            else
            {
                Stream fs = null;
                try
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    fs = new FileStream(Filename, FileMode.Create);
                    serializer.Serialize(fs, Instance);
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return retVal;
        }

        /// <summary>
        /// Overload that supports passing in an XML TextWriter. Note the Writer is not closed
        /// </summary>
        /// <param name="Instance"></param>
        /// <param name="writer"></param>
        /// <param name="BinarySerialization"></param>
        /// <returns></returns>
        public static bool SerializeObject(object Instance, XmlTextWriter writer)
        {
            bool retVal = true;

            try
            {
                XmlSerializer serializer =
                    new XmlSerializer(Instance.GetType());

                // Create an XmlTextWriter using a FileStream.
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 3;

                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, Instance);
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                retVal = false;
            }

            return retVal;
        }

        /// <summary>
        /// Serializes an object into a string variable for easy 'manual' serialization
        /// </summary>
        /// <param name="Instance"></param>
        /// <returns></returns>
        public static bool SerializeObject(object Instance, out string XmlResultString)
        {
            XmlResultString = "";
            MemoryStream ms = new MemoryStream();

            XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding());

            if (!SerializeObject(Instance, writer))
            {
                ms.Close();
                return false;
            }

            byte[] Result = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Result, 0, (int)ms.Length);

            XmlResultString = Encoding.UTF8.GetString(Result, 0, (int)ms.Length);

            ms.Close();
            writer.Close();

            return true;
        }


        /// <summary>
        /// Serializes an object instance to a file.
        /// </summary>
        /// <param name="Instance">the object instance to serialize</param>
        /// <param name="Filename"></param>
        /// <param name="BinarySerialization">determines whether XML serialization or binary serialization is used</param>
        /// <returns></returns>
        public static bool SerializeObject(object Instance, out byte[] ResultBuffer)
        {
            bool retVal = true;

            MemoryStream ms = null;
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                serializer.Serialize(ms, Instance);
            }
            catch(Exception ex)
            {                
                retVal = false;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }

            ResultBuffer = ms.ToArray();

            return retVal;
        }

        /// <summary>
        /// Deserializes an object from file and returns a reference.
        /// </summary>
        /// <param name="Filename">name of the file to serialize to</param>
        /// <param name="ObjectType">The Type of the object. Use typeof(yourobject class)</param>
        /// <param name="BinarySerialization">determines whether we use Xml or Binary serialization</param>
        /// <returns>Instance of the deserialized object or null. Must be cast to your object type</returns>
        public static object DeSerializeObject(string Filename, Type ObjectType, bool BinarySerialization)
        {
            object Instance = null;

            if (!BinarySerialization)
            {

                XmlReader reader = null;
                XmlSerializer serializer = null;
                FileStream fs = null;
                try
                {
                    // Create an instance of the XmlSerializer specifying type and namespace.
                    serializer = new XmlSerializer(ObjectType);

                    // A FileStream is needed to read the XML document.
                    fs = new FileStream(Filename, FileMode.Open);
                    reader = new XmlTextReader(fs);

                    Instance = serializer.Deserialize(reader);

                }
                catch (Exception ex)
                {
                    string Message = ex.Message;
                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();

                    if (reader != null)
                        reader.Close();
                }
            }
            else
            {

                BinaryFormatter serializer = null;
                FileStream fs = null;

                try
                {
                    serializer = new BinaryFormatter();
                    fs = new FileStream(Filename, FileMode.Open);
                    Instance = serializer.Deserialize(fs);

                }
                catch
                {
                    return null;
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return Instance;
        }

        /// <summary>
        /// Deserialize an object from an XmlReader object.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="ObjectType"></param>
        /// <returns></returns>
        public static object DeSerializeObject(System.Xml.XmlReader reader, Type ObjectType)
        {
            XmlSerializer serializer = new XmlSerializer(ObjectType);
            object Instance = serializer.Deserialize(reader);
            reader.Close();

            return Instance;
        }

        public static object DeSerializeObject(string XML, Type ObjectType)
        {
            XmlTextReader reader = new XmlTextReader(XML, XmlNodeType.Document, null);
            return DeSerializeObject(reader, ObjectType);
        }

        public static object DeSerializeObject(byte[] Buffer, Type ObjectType)
        {
            BinaryFormatter serializer = null;
            MemoryStream ms = null;
            object Instance = null;

            try
            {
                serializer = new BinaryFormatter();
                ms = new MemoryStream(Buffer);
                Instance = serializer.Deserialize(ms);

            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }

            return Instance;
        }
      
    }
}





