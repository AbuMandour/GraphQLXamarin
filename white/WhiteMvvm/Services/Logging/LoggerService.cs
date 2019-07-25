using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using WhiteMvvm.Services.Dialog;

namespace WhiteMvvm.Services.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly IDialogService _dialogService;

        public LoggerService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public async Task LogException(Exception exception)
        {
            Crashes.TrackError(exception);
#if DEBUG
            await _dialogService.ShowErrorAsync(exception.ToString());
            Console.WriteLine(exception.ToString());
#endif
        }
    }
}
