using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sockets
{
    
    class Protocol
    {
        public byte opcode;
        public byte idOrigin;
        public byte[] idDestination = new byte[8];
        public string message;

        //transforma todas informacoes em um unico pacote de bytes
        public byte[] parseData(byte opcode, byte idOrigin, byte[] idDestination, byte[] message)
        {
            byte[] output = new byte[1024];


            output[0] = opcode;
            output[1] = idOrigin;

            //concatena vetor de destino com os vetore restantes
            System.Buffer.BlockCopy(idDestination,  0, output, 2, 8);
            System.Buffer.BlockCopy(message,        0, output, 1, 10);

            return output;
        }

        //retira do pacote as informacoes pertinentes
        public bool unParseData(byte[] data)
        {
            byte[] messageByteFormat = new byte[1014];

            //se o pacote for maior que 1024 bytes, ou menor que 11 (que seria o tamanho mínimo de mensagem válida)
            if (data.Length > 1024 || data.Length < 11)
                return false;

            else
            {
                opcode = data[0];
                idOrigin = data[1];

                System.Buffer.BlockCopy(data, 2, idDestination, 0, 8);
                System.Buffer.BlockCopy(data, 10 , messageByteFormat, 0, data.Length - 10);

                message = Encoding.ASCII.GetString(messageByteFormat);

                return true;
            }
        }
        
    }


}
