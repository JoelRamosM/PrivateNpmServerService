using System.Diagnostics;
using Topshelf;

namespace NPM.Server
{
    public class ServerRunner
    {
        private readonly Process _process;
        private readonly ProcessStartInfo _startInfo;

        public string Host { get; set; } = "172.30.0.128:4873";

        public ServerRunner()
        {
            _process = new Process();
            _startInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C sinopia -l {Host}"
            };
            _process.StartInfo = _startInfo;
        }

        public void Start() => _process.Start();

        public void Stop() => _process.Close();
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServerRunner>(s =>
                {
                    s.ConstructUsing(name => new ServerRunner());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsNetworkService();

                x.SetDescription("[STAGING] FORTES NPM SERVER");
                x.SetDisplayName("[STAGING] FORTES NPM SERVER");
                x.SetServiceName("[STAGING] FORTES NPM SERVER");
            });
        }

        public static void StartServer(string host)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C sinopia -l {host}"
            };
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}