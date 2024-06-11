namespace WakaTime_UI.Repositories.ControlUserId
{
    public class EnsureUserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public EnsureUserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();
            if (!path.StartsWith("/login") && context.Session.GetInt32("UserId") == null)
            {
                context.Response.Redirect("/Login");
                return;
            }
            await _next(context);
        }
    }

}
