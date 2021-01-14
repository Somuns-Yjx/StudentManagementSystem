using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Data;

namespace StuMgmLib.MyNameSpace
{
    public class BinaryED // 序列化与反序列化
    {
        /// <summary>
        ///  序列化
        /// </summary>
        public static byte[] Serialize<T>(T c)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter iFormatter = new BinaryFormatter();
            iFormatter.Serialize(ms, c);
            byte[] buf = ms.GetBuffer();
            return buf;
        }

        /// <summary>
        ///  反序列化
        /// </summary>
        public static T Deserialize<T>(byte[] buf)
        {
            MemoryStream ms = new MemoryStream(buf);
            BinaryFormatter iFormatter = new BinaryFormatter();
            var obj = (T)iFormatter.Deserialize(ms);
            return obj;
        }
    }




}
