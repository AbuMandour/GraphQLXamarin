﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WhiteMvvm.Utilities
{
    public class TaskCommand : ICommand
    {
        #region Fields
        private volatile bool _inProgress = false;
        private Func<object, Task> _execute;
        private bool _continueOnCapturedContext;
        private Func<object, bool> _canExecute;
        private Action onItemSelected;

        public TaskCommand(Action onItemSelected)
        {
            this.onItemSelected = onItemSelected;
        }
        #endregion

        public TaskCommand(Func<object, Task> execute, Func<object, bool> canExecute = null, bool continueOnCapturedContext = true)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            _continueOnCapturedContext = continueOnCapturedContext;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (_inProgress)
            {
                return false;
            }
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }
        public void Execute(object parameter)
        {
            _inProgress = true;
            RaiseCanExecuteChanged();
            _execute(parameter).ContinueWith((task) =>
            {
                _inProgress = false;
                RaiseCanExecuteChanged();
            }).ConfigureAwait(_continueOnCapturedContext);
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(null, new EventArgs());
        }
    }
}
