using Castle.DynamicProxy;

namespace Ioc.Interceptors
{
	class RepoInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();
		}
	}
}
