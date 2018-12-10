using DomoServer.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Red
{
    class PacketWriter
    {
        private MemoryStream memoryStream;
        private BinaryWriter binaryWriter;

        public PacketWriter()
        {
            memoryStream = new MemoryStream();
            binaryWriter = new BinaryWriter(memoryStream);
        }

        public long Position
        {
            get
            {
                return memoryStream.Position;
            }
            set
            {
                memoryStream.Position = value;
            }

        }
        public long Lenght
        {
            get
            {
                return memoryStream.Length;
            }

        }

        public byte[] Compile()
        {
            return memoryStream.ToArray();

        }

        public void WriteInt32(int value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public void WriteInt16(Int16 value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public void WriteInt16(int value)
        {
            try
            {
                binaryWriter.Write((Int16)value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public void WriteInt64(Int64 value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public void WriteShort(short value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public void WriteByte(byte value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }

        }
        public void WriteByte(int value)
        {
            try
            {
                binaryWriter.Write(((byte)(value)));
            }
            catch (Exception exeption)
            {
                throw exeption;
            }

        }
        public void WriteDouble(double value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public void WriteFloat(float value)
        {
            try
            {
                binaryWriter.Write(value);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public void WriteLong(long val)
        {
            try
            {
                binaryWriter.Write(val);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        public void WriteString(string text, int limit)
        {
            try
            {
                if (text == null)
                {
                    binaryWriter.Write((short)0);
                }
                else
                {
                    binaryWriter.Write(ConvertidorTipos.StringToByteArray(text, limit));
                }
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }

        public void WriteString(string text)
        {
            try
            {
                if (text == null)
                {
                    binaryWriter.Write((short)0);
                }
                else
                {
                    Encoding encoding = Encoding.ASCII;
                    binaryWriter.Write(encoding.GetBytes(text));
                    binaryWriter.Write((short)0);
                }
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
        
        public void WriteBytes(byte[] data)
        {
            try
            {
                binaryWriter.Write(data);
            }
            catch (Exception exeption)
            {
                throw exeption;
            }
        }
    }
}
