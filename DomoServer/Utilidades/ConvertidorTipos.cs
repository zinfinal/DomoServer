using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoServer.Utilidades
{
    class ConvertidorTipos
    {
        public static string ByteArrayToString(byte[] data)
        {
            try
            {
                return System.Text.Encoding.ASCII.GetString(data).TrimEnd('\0');
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static char[] StringToCharArray(string text)
        {
            try
            {

                return text.ToCharArray();

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static byte CharToByte(char value)
        {
            try
            {

                return Convert.ToByte(value);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public static byte[] StringToByteArray(string text, int limitBytes)
        {
            try
            {

                char[] stringCharArray = StringToCharArray(text);
                if (stringCharArray.Length.Equals(0))
                {
                    throw new Exception("Failed");
                }
                byte[] charArrayBytes = new byte[limitBytes];
                for (int i = 0; i < limitBytes; i++)
                {
                    if (i < stringCharArray.Length)
                    {
                        charArrayBytes[i] = CharToByte(stringCharArray[i]);
                    }
                    else
                    {
                        charArrayBytes[i] = 0x00;
                    }
                }
                return charArrayBytes;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
