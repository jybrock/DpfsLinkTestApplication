#region usings
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
#endregion


///  <summary>
/// This is a simple TCP and UDP based client.
///  </summary>
namespace DpfsLinkTestApplication {
    public class ClientSocket {

        /// <summary>
        /// This routine repeatedly copies a string message into a byte array until filled.
        /// </summary>
        /// <param name="dataBuffer">Byte buffer to fill with string message</param>
        /// <param name="message">String message to copy</param>
        static public void FormatBuffer(byte[] dataBuffer, string message) {
            byte[] byteMessage = System.Text.Encoding.ASCII.GetBytes(message);              // Pull message into encoded Byte[0]
            int index = 0;                                                                  // Start counter for dataBuffer[]

            // First convert the string to bytes and then copy into send buffer
            while (index < dataBuffer.Length) {                                             // Loop int with [int].Length
                for (int j = 0; j < byteMessage.Length; j++) {
                    dataBuffer[index] = byteMessage[j];
                    index++;
                    // Make sure we don't go past the send buffer length
                    if (index >= dataBuffer.Length) {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Prints simple usage information.
        /// </summary>
        static public Queue FillClientUsage() {
            Queue queUs = new Queue();
            queUs.Enqueue("Loading Udp/Tcp Client Command Usage...");
            queUs.Enqueue("usage: Executable_file_name [-c] [-n server] [-p port] [-m message] [-t tcp|udp] [-x size]");
            queUs.Enqueue("     -c              If UDP connect the socket before sending");
            queUs.Enqueue("     -n server       Server name or address to connect/send to");
            queUs.Enqueue("     -p port         Port number to connect/send to");
            queUs.Enqueue("     -m message      String message to format in request buffer");
            queUs.Enqueue("     -t tcp|udp      Indicates to use either the TCP or UDP protocol");
            queUs.Enqueue("     -x size         Size of send and receive buffers");
            queUs.Enqueue(" Else, default values will be used...");
            queUs.Enqueue("");
            queUs.Enqueue("Finished loading Udp/Tcp Client Command Usage.");
            queUs.Enqueue("Exit[0]");

            return queUs;
        }

        /// <summary>
        /// This is the main function for the simple client. It parses the command line and creates
        /// a socket of the requested type. For TCP, it will resolve the name and attempt to connect
        /// to each resolved address until a successful connection is made. Once connected a request
        /// message is sent followed by shutting down the send connection. The client then receives
        /// data until the server closes its side at which point the client socket is closed. For
        /// UDP, the socket is created and if indicated connected to the server's address. A single
        /// request datagram message. The client then waits to receive a response and continues to
        /// do so until a zero byte datagram is receive which indicates the end of the response.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args) {
            //SocketType sockType = SocketType.Stream;
            //ProtocolType sockProtocol = ProtocolType.Tcp;
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;
            string remoteName = "localhost", textMessage = "Client: This is a test";
            bool udpConnect = false;
            int remotePort = 5150, bufferSize = 4096;
            
            Console.WriteLine();
            FillClientUsage();
            Console.WriteLine();

            // Parse the command line
            for (int i = 0; i < args.Length; i++) {
                try {
                    if ((args[i][0] == '-') || (args[i][0] == '/')) {
                        switch (Char.ToLower(args[i][1])) {
                            case 'c':       // "Connect" the UDP socket to the destination
                                udpConnect = true;
                                break;
                            case 'n':       // Destination address to connect to or send to
                                remoteName = args[++i];
                                break;
                            case 'm':       // Text message to put into the send buffer
                                textMessage = args[++i];
                                break;
                            case 'p':       // Port number for the destination
                                remotePort = System.Convert.ToInt32(args[++i]);
                                break;
                            case 't':       // Specified TCP or UDP
                                i++;
                                if (String.Compare(args[i], "tcp", true) == 0) {
                                    sockType = SocketType.Stream;
                                    sockProtocol = ProtocolType.Tcp;
                                } else if (String.Compare(args[i], "udp", true) == 0) {
                                    sockType = SocketType.Dgram;
                                    sockProtocol = ProtocolType.Udp;
                                } else {
                                    FillClientUsage();
                                    return;
                                }
                                break;
                            case 'x':       // Size of the send and receive buffers
                                bufferSize = System.Convert.ToInt32(args[++i]);
                                break;
                            default:
                                FillClientUsage();
                                return;
                                
                        }
                    }
                } catch {
                    FillClientUsage();
                    return;
                }
            }

            Socket clientSocket = null;
            IPHostEntry resolvedHost = null;
            IPEndPoint destination = null;
            byte[] sendBuffer = new byte[bufferSize], recvBuffer = new Byte[bufferSize];
            int rc;

            // Format the string message into the send buffer
            FormatBuffer(sendBuffer, textMessage);

            try {
                // Try to resolve the remote host name or address
                resolvedHost = Dns.GetHostEntry(remoteName);
                Console.WriteLine("Client: GetHostEntry() is OK...");

                // Try each address returned
                foreach (IPAddress addr in resolvedHost.AddressList) {
                    // Create a socket corresponding to the address family of the resolved address
                    clientSocket = new Socket(
                        addr.AddressFamily,
                        sockType,
                        sockProtocol
                        );
                    Console.WriteLine("Client: Socket() is OK...");
                    try {
                        // Create the endpoint that describes the destination
                        destination = new IPEndPoint(addr, remotePort);
                        Console.WriteLine("Client: IPEndPoint() is OK. IP Address: {0}, server port: {1}", addr, remotePort);

                        if ((sockProtocol == ProtocolType.Udp) && (udpConnect == false)) {
                            Console.WriteLine("Client: Destination address is: {0}", destination.ToString());
                            break;
                        } else {
                            Console.WriteLine("Client: Attempting connection to: {0}", destination.ToString());
                        }
                        clientSocket.Connect(destination);
                        Console.WriteLine("Client: Connect() is OK...");
                        break;
                    } catch (SocketException) {
                        // Connect failed, so close the socket and try the next address
                        clientSocket.Close();
                        Console.WriteLine("Client: Close() is OK...");
                        clientSocket = null;
                        continue;
                    }
                }

                // Make sure we have a valid socket before trying to use it
                if ((clientSocket != null) && (destination != null)) {
                    try {
                        // Send the request to the server
                        if ((sockProtocol == ProtocolType.Udp) && (udpConnect == false)) {
                            clientSocket.SendTo(sendBuffer, destination);
                            Console.WriteLine("Client: SendTo() is OK...UDP...");
                        } else {
                            rc = clientSocket.Send(sendBuffer);
                            Console.WriteLine("Client: send() is OK...TCP...");
                            Console.WriteLine("Client: Sent request of {0} bytes", rc);

                            // For TCP, shutdown sending on our side since the client won't send any more data
                            if (sockProtocol == ProtocolType.Tcp) {
                                clientSocket.Shutdown(SocketShutdown.Send);
                                Console.WriteLine("Client: Shutdown() is OK...");
                            }
                        }

                        // Receive data in a loop until the server closes the connection. For
                        //    TCP this occurs when the server performs a shutdown or closes
                        //    the socket. For UDP, we'll know to exit when the remote host
                        //    sends a zero byte datagram.
                        while (true) {
                            if ((sockProtocol == ProtocolType.Tcp) || (udpConnect == true)) {
                                rc = clientSocket.Receive(recvBuffer);

                                Console.WriteLine("Client: Receive() is OK...");
                                Console.WriteLine("Client: Read {0} bytes", rc);
                            } else {
                                IPEndPoint fromEndPoint = new IPEndPoint(destination.Address, 0);
                                Console.WriteLine("Client: IPEndPoint() is OK...");
                                EndPoint castFromEndPoint = (EndPoint)fromEndPoint;
                                rc = clientSocket.ReceiveFrom(recvBuffer, ref castFromEndPoint);
                                Console.WriteLine("Client: ReceiveFrom() is OK...");
                                fromEndPoint = (IPEndPoint)castFromEndPoint;
                                Console.WriteLine("Client: Read {0} bytes from {1}", rc, fromEndPoint.ToString());
                            }

                            // Exit loop if server indicates shutdown
                            if (rc == 0) {
                                clientSocket.Close();
                                Console.WriteLine("Client: Close() is OK...");
                                break;
                            }
                        }
                    } catch (SocketException err) {
                        Console.WriteLine("Client: Error occurred while sending or receiving data.");
                        Console.WriteLine("   Error: {0}", err.Message);
                    }
                } else {
                    Console.WriteLine("Client: Unable to establish connection to server!");
                }
            } catch (SocketException err) {
                Console.WriteLine("Client: Socket error occurred: {0}", err.Message);
            }
        }

        /// <summary>
        /// Create Manual Pages for ClientSocket
        /// </summary>
        /// <returns></returns>
        public static Queue FillConsoleIni() {
            //make object of the queue class
            Queue manInitialize = new Queue();
            manInitialize.Enqueue(" ");
            manInitialize.Enqueue("// This is a simple TCP client application. For TCP, the server name is");
            //manInitialize.Enqueue("// This is a simple TCP and UDP client application. For TCP, the server name is");
            manInitialize.Enqueue("// resolved and a socket is created to attempt a connection to each address");
            manInitialize.Enqueue("// returned until a connection succeeds. Once connected the client sends a 'request'");
            manInitialize.Enqueue("// message to the server and shuts down the sending side. The client then loops");
            manInitialize.Enqueue("// to receive the server response until the server closes the connection at which");
            //manInitialize.Enqueue("// point the client closes its socket and exits. For UDP, the server name is resolved");
            //manInitialize.Enqueue("// and the first address returned is used (since there is no indication that there");
            //manInitialize.Enqueue("// is a UDP server at the endpoint). The UDP client then sends a single datagram");
            //manInitialize.Enqueue("// 'request' message and then waits to receive a response from the server. The client");
            manInitialize.Enqueue("// continues to receive until a zero byte datagram is received. Note that the server");
            manInitialize.Enqueue("// sends several zero byte datagrams but if they are lost, the client will never");
            manInitialize.Enqueue("// exit.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// usage:");
            manInitialize.Enqueue("//      Executable_file_name [-c] [-n server]|[-l server] [-p port] [-m message] [-t tcp|udp] [-x size]");
            manInitialize.Enqueue("//");
            //manInitialize.Enqueue("//           -c                         If UDP connect the socket before sending");
            manInitialize.Enqueue("//           -c                         UDP only");
            manInitialize.Enqueue("//           -n server             Server name to connect/send to");
            manInitialize.Enqueue("//           -l server             Server address to connect/send to (IPv4 or IPv6)");
            manInitialize.Enqueue("//           -p port                 Port number to connect/send to");
            manInitialize.Enqueue("//           -m message      String message to format in request buffer");
            manInitialize.Enqueue("//           -t tcp|udp            Indicates to use either the TCP or UDP protocol");
            manInitialize.Enqueue("//           -x size                 Size of send and receive buffers");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("// sample usage:");
            manInitialize.Enqueue("//      The following command line establishes a TCP connection to the given server");
            manInitialize.Enqueue("//      on port 5150. The other two command lines are sample server command lines --");
            manInitialize.Enqueue("//      one for IPv4 and one for IPv6. Since the client will attempt to resolve");
            manInitialize.Enqueue("//      the server's name, it should attempt to connect over IPv4 and IPv6 as long");
            manInitialize.Enqueue("//      as the addresses are registered in DNS.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -n server -p 5150 -t tcp");
            manInitialize.Enqueue("//          Executable_file_name -l :: -p 5150 -t tcp");
            manInitialize.Enqueue("//          Executable_file_name -l 0.0.0.0 -p 5150 -t tcp");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//      The following command line creates a connected UDP socket that sends");
            manInitialize.Enqueue("//      data to the server x.y.z.w on port 5150. While the second entry is the");
            manInitialize.Enqueue("//      server command line used. ");
            //manInitialize.Enqueue("//      NOTE: For UDP sockets, the buffer size on the client and server should match");
            //manInitialize.Enqueue("//            as an exception will be thrown if the receiving buffer is smaller than");
            //manInitialize.Enqueue("//            the incoming datagram.");
            manInitialize.Enqueue("//");
            manInitialize.Enqueue("//          Executable_file_name -n x.y.z.w -p 5150 -t udp -c -x 512");
            manInitialize.Enqueue("//          Executable_file_name -l x.y.z.w -p 5150 -t udp -x 512");
            manInitialize.Enqueue("//");

            return manInitialize;
        }
    }
}
