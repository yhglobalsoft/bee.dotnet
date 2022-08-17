// using System.Diagnostics;
// using Bee.Hangfire.OpenTelemetry;
// using OpenTelemetry.Trace;
//
// namespace Bee.Hangfire.Diagnostics.Sample;
//
// public static class TracerProviderBuilderExtensions
// {
//     public static TracerProviderBuilder AddBeeInstrumentation(this TracerProviderBuilder builder)
//     {
//         if (builder == null)
//         {
//             throw new ArgumentNullException(nameof(builder));
//         }
//
//         DiagnosticListener.AllListeners.Subscribe(new BeeHangfireDiagnosticSourceSubscriber());
//         builder.AddSource(BeeHangfireDiagnosticListenerConsts.SourceName);
//         return builder.AddInstrumentation(() => new BeeHangfireDiagnosticSourceSubscriber());
//     }
// }