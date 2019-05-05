using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.Controllers
{
    public class ErrorController : Controller
    {
        #region 4XX Status Code

        /// <summary>
        /// 400 Bad Request
        /// </summary>
        /// <returns></returns>
        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }

        /// <summary>
        /// 401 Unauthorized
        /// </summary>
        /// <returns></returns>
        public ActionResult Unauthorized()
        {
            Response.StatusCode = 401;
            return View();
        }

        /// <summary>
        /// 402 Payment Required
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentRequired()
        {
            Response.StatusCode = 402;
            return View();
        }

        /// <summary>
        /// 403 Forbidden
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        /// <summary>
        /// 404 Not Found
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// 414 URI Too Long
        /// </summary>
        /// <returns></returns>
        public ActionResult URITooLong()
        {
            Response.StatusCode = 414;
            return View();
        }

        #endregion


        #region 5XX Status Code

        /// <summary>
        /// 500 Internal Server Error
        /// </summary>
        /// <returns></returns>
        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }

        /// <summary>
        /// 501 Not Implemented
        /// </summary>
        /// <returns></returns>
        public ActionResult NotImplemented()
        {
            Response.StatusCode = 501;
            return View();
        }

        /// <summary>
        /// 502 Bad Gateway
        /// </summary>
        /// <returns></returns>
        public ActionResult BadGateway()
        {
            Response.StatusCode = 502;
            return View();
        }

        /// <summary>
        /// 520 - Uknown Error
        /// </summary>
        /// <returns></returns>
        public ActionResult UknownError()
        {
            Response.StatusCode = 520;
            return View();
        }

        /// <summary>
        /// 521 - Web Server Is Down
        /// </summary>
        /// <returns></returns>
        public ActionResult WebServerIsDown()
        {
            Response.StatusCode = 521;
            return View();
        }

        /// <summary>
        /// 522 Connection Timed Out
        /// </summary>
        /// <returns></returns>
        public ActionResult ConnectionTimedOut()
        {
            Response.StatusCode = 522;
            return View();
        }

        /// <summary>
        /// 524 A Time Occured
        /// </summary>
        /// <returns></returns>
        public ActionResult ATimeOccurred()
        {
            Response.StatusCode = 524;
            return View();
        } 

        #endregion
    }
}