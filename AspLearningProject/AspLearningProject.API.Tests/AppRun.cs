using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspLearningProject.API.Tests
{
    public sealed class AppRun: IDisposable
    {
        private const string PathToProject = "C:\\Users\\Tatiana_Budnikova.EPAM\\Desktop\\ASPLearning\\AspLearningProject\\AspLearningProject\\AspLearningProject.csproj";
        private Process process;

        public AppRun()
        {
            process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo = new ProcessStartInfo("dotnet.exe",
                $"run --project {PathToProject}")
            {
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            process.Start();
        }

        public void Dispose()
        {
          process.Kill();
        }
        
    }
}
