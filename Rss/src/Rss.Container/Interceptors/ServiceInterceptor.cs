using Castle.DynamicProxy;
using System;

namespace Ioc.Interceptors
{
	class ServiceInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
		}
	}
}
