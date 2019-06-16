using System;

namespace GUI.Base
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}