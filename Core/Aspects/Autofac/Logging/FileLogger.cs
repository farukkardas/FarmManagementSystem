using System;
using System.IO;
using System.Web;
using System.Text;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;

namespace Core.Aspects.Autofac.Logging
{
    public class FileLogger : MethodInterception
    {
        private readonly Type _userInfo;

        public FileLogger(Type userInfo)
        {
            _userInfo = userInfo;
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            MethodInvokedSuccessfully(invocation.Method.Name);
            LogLogin(invocation);
        }
        
        protected override void OnException(IInvocation invocation, Exception e)
        {
            LogExceptions(invocation.Method.Name, e);
        }

        private async void LogExceptions(string methodName, Exception e)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string specificFolder = Path.Combine(appData, "FMSLogs");
            string auths = Path.Combine(specificFolder, "Exceptions");
            Directory.CreateDirectory(auths);

            //Günlük giriş verilerini logluyor
            var dateToday = DateTime.Now.ToShortDateString();
            var trim = dateToday.Replace(".", "");
            var fileName = auths + "\\" + trim + ".txt";

            if (!File.Exists(fileName))
            {
                await using FileStream fs = File.Create(fileName);
                var success =
                    new UTF8Encoding(true).GetBytes($"[{DateTime.Now}] Method: {methodName} - Error: {e.Message} ");
                await fs.WriteAsync(success);
                fs.Close();
            }

            else
            {
                await using StreamWriter data = new StreamWriter(fileName, true);
                await data.WriteAsync($"[{DateTime.Now}] Method: {methodName} - Error: {e.Message} ");
                data.Close();
            }
        }
        
        private void LogLogin(IInvocation invocation)
        {
            var infos = _userInfo.GetProperties();
            var email = infos[0].GetValue(invocation.Arguments[0]);
            if (email != null) AuthLogs(email.ToString());
        }

        private static async void AuthLogs(string email)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string specificFolder = Path.Combine(appData, "FMSLogs");
            string auths = Path.Combine(specificFolder, "Logins");
            Directory.CreateDirectory(auths);

            //Günlük giriş verilerini logluyor
            var dateToday = DateTime.Now.ToShortDateString();
            var trim = dateToday.Replace(".", "");
            var fileName = auths + "\\" + trim + ".txt";

            if (!File.Exists(fileName))
            {
                await using FileStream fs = File.Create(fileName);
                var success =
                    new UTF8Encoding(true).GetBytes($"[{DateTime.Now}]  {email} successfully logged.");
                await fs.WriteAsync(success);
                fs.Close();
            }

            else
            {
                await using StreamWriter data = new StreamWriter(fileName, true);
                await data.WriteAsync($"\n[{DateTime.Now}]  {email} successfully logged.");
                data.Close();
            }
        }

        private async void MethodInvokedSuccessfully(string methodName)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string specificFolder = Path.Combine(appData, "FMSLogs");
            string auths = Path.Combine(specificFolder, "Methods");
            Directory.CreateDirectory(auths);

            var dateToday = DateTime.Now.ToShortDateString();
            var trim = dateToday.Replace(".", "");
            var fileName = auths + "\\" + trim + ".txt";


            if (!File.Exists(fileName))
            {
                await using FileStream fs = File.Create(fileName);
                var success =
                    new UTF8Encoding(true).GetBytes(
                        $"[{DateTime.Now}]  {methodName} operation called successfully.");
                await fs.WriteAsync(success);
                fs.Close();
            }

            else
            {
                await using StreamWriter data = new StreamWriter(fileName, true);
                await data.WriteAsync($"\n[{DateTime.Now}]  {methodName} operation called successfully.");
            }
        }
    }
}