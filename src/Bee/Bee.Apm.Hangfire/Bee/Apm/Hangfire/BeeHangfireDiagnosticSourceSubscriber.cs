namespace Bee.Apm.Hangfire
{
    public class BeeHangfireDiagnosticSourceSubscriber : IDisposable, IObserver<System.Diagnostics.DiagnosticListener>
    {
        private IDisposable _sourcesSubscription;

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value.Name == BeeHangfireDiagnosticConsts.DiagnosticListenerName)
            {
                _sourcesSubscription = value.Subscribe(new BeeHangfireDiagnosticListener());
            }
        }

        public void Dispose()
        {
            _sourcesSubscription?.Dispose();
        }
    }
}