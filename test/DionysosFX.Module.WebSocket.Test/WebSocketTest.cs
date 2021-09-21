using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DionysosFX.Module.WebSocket.Test
{
    public class WebSocketTest
    {
        private async Task<ClientWebSocket> Connect()
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();
            try
            {
                await clientWebSocket.ConnectAsync(new Uri("ws://localhost:1923/chat"), new CancellationToken());
            }
            catch (Exception)
            {
            }
            return clientWebSocket;
        }

        private CancellationToken Token => new CancellationTokenSource(2500).Token;

        [Fact]
        public async void AddGroupTest()
        {
            var ws = await Connect();
            if (ws.State != WebSocketState.Open)
                Assert.True(false);
            try
            {
                byte[] buffer = new byte[2048];
                buffer = Encoding.UTF8.GetBytes("action=AddGroup&value=Admin");
                await ws.SendAsync(buffer, WebSocketMessageType.Text, true, Token);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void RemoveGroupTest()
        {
            var ws = await Connect();
            if (ws.State != WebSocketState.Open)
                Assert.True(false);
            try
            {
                byte[] buffer = new byte[2048];
                buffer = Encoding.UTF8.GetBytes("action=RemoveGroup&value=Admin");
                await ws.SendAsync(buffer, WebSocketMessageType.Text, true, Token);
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void SendTest()
        {
            var ws = await Connect();
            if (ws.State != WebSocketState.Open)
                Assert.True(false);
            try
            {
                Task<bool> task1 = Task.Run(() =>
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            WebSocketReceiveResult result;
                            do
                            {
                                byte[] buffer = new byte[4096];
                                result = ws.ReceiveAsync(buffer,Token).Result;
                                ms.Write(buffer, 0, result.Count);
                            } while (!result.EndOfMessage);

                            if (result.MessageType == WebSocketMessageType.Text)
                            {
                                var message = Encoding.UTF8.GetString(ms.ToArray());
                                return !string.IsNullOrEmpty(message);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return false;
                });
                Task task2 = Task.Run(() =>
                {
                    try
                    {
                        Thread.Sleep(250);
                        byte[] buffer = new byte[2048];
                        buffer = Encoding.UTF8.GetBytes("action=Send&value=1&message=Hello World");
                        ws.SendAsync(buffer, WebSocketMessageType.Text, true,Token);
                    }
                    catch (Exception)
                    {
                    }
                });
                Task.WaitAll(task1, task2);
                Assert.True(task1.Result);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void SendOtherTest()
        {
            var ws = await Connect();
            if (ws.State != WebSocketState.Open)
                Assert.True(false);
            try
            {
                Task<bool> task1 = Task.Run(() =>
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebSocketReceiveResult result;
                        do
                        {
                            try
                            {
                                byte[] buffer = new byte[4096];
                                result = ws.ReceiveAsync(buffer, Token).Result;
                                return false;
                            }
                            catch (Exception)
                            {
                                return true;
                            }
                        } while (!result.EndOfMessage);
                    }
                });
                Task task2 = Task.Run(() =>
                {
                    try
                    {
                        Thread.Sleep(250);
                        byte[] buffer = new byte[2048];
                        buffer = Encoding.UTF8.GetBytes("action=SendOther&value=1&message=Hello World");
                        ws.SendAsync(buffer, WebSocketMessageType.Text, true, new CancellationToken());
                    }
                    catch (Exception)
                    {
                    }
                });
                Task.WaitAll(task1, task2);
                Assert.True(task1.Result);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void SendToGroupTest()
        {
            var ws = await Connect();
            if (ws.State != WebSocketState.Open)
                Assert.True(false);
            try
            {
                Task<bool> task1 = Task.Run(() =>
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            WebSocketReceiveResult result;
                            do
                            {
                                byte[] buffer = new byte[4096];
                                result = ws.ReceiveAsync(buffer,Token).Result;
                                ms.Write(buffer, 0, result.Count);
                            } while (!result.EndOfMessage);

                            if (result.MessageType == WebSocketMessageType.Text)
                            {
                                var message = Encoding.UTF8.GetString(ms.ToArray());
                                return !string.IsNullOrEmpty(message);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return false;
                });
                Task task2 = Task.Run(() =>
                {
                    try
                    {
                        Thread.Sleep(500);
                        byte[] buffer = new byte[2048];
                        buffer = Encoding.UTF8.GetBytes("action=AddGroup&value=Admin");
                        ws.SendAsync(buffer, WebSocketMessageType.Text, true, new CancellationToken());
                        buffer = Encoding.UTF8.GetBytes("action=SendGroup&value=Admin&message=Hello World");
                        ws.SendAsync(buffer, WebSocketMessageType.Text, true, new CancellationToken());
                    }
                    catch (Exception)
                    {
                    }
                });
                Task.WaitAll(task1, task2);
                Assert.True(task1.Result);
            }
            catch (Exception e)
            {
                Assert.True(false);
            }
        }
    }
}
