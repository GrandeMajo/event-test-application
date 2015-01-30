using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Windows;

namespace WpfApplication1
{

    //[Serializable]
    //public class DataContainer : ISerializable
    //{
    //    public object Data { get; set; }

    //    public DataContainer(object data)
    //    {
    //        Data = data;
    //    }

    //    // Deserialization constructor
    //    protected DataContainer(SerializationInfo info, StreamingContext context)
    //    {
    //        IntPtr address = (IntPtr)info.GetValue("dataAddress", typeof(IntPtr));
    //        GCHandle handle = GCHandle.FromIntPtr(address);
    //        Data = handle.Target;
    //        handle.Free();
    //    }

    //    #region ISerializable Members

    //    public void GetObjectData(SerializationInfo info, StreamingContext context)
    //    {
    //        GCHandle handle = GCHandle.Alloc(Data);
    //        IntPtr address = GCHandle.ToIntPtr(handle);
    //        info.AddValue("dataAddress", address);
    //    }

    //    #endregion
    //}
    
    [Serializable]
    public class MyDataObject : IDataObject
    {
        System.Collections.Hashtable _Data = new System.Collections.Hashtable();

        public MyDataObject() { }

        public MyDataObject(object data)
        {
            SetData(data);
        }

        public MyDataObject(object data, string format)
        {
            SetData(data, format);
        }

        #region IDataObject Members

        public object GetData(Type format)
        {
            return _Data[format.FullName];
        }

        public bool GetDataPresent(Type format)
        {
            return _Data.ContainsKey(format.FullName);
        }

        public string[] GetFormats()
        {
            string[] strArray = new string[_Data.Keys.Count];
            _Data.Keys.CopyTo(strArray, 0);
            return strArray;
        }

        public string[] GetFormats(bool autoConvert)
        {
            return GetFormats();
        }

        private void SetData(object data, string format)
        {
            _Data[format] = data;
        }

        public void SetData(object data)
        {
            Type type = data.GetType();
            SetData(data, type.FullName);
        }

        #endregion

        public object GetData(string format, bool autoConvert)
        {
            return _Data[format];
        }

        public object GetData(string format)
        {
            return GetData(format, true);
        }

        public bool GetDataPresent(string format, bool autoConvert)
        {
            return _Data.ContainsKey(format);
        }

        public bool GetDataPresent(string format)
        {
            return GetDataPresent(format, true);
        }

        public void SetData(string format, object data, bool autoConvert)
        {
            _Data.Add(format, data);
        }

        public void SetData(Type format, object data)
        {
            _Data.Add(format.FullName, data);
        }

        public void SetData(string format, object data)
        {
            SetData(format, data, true);
        }
    }
}
