﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IsisTravelCore.Data;
using IsisTravelCore.Models.Domains;
using IsisTravelCore.Services;

namespace IsisTravelCore.Controllers
{
    public class RequestOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestOffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RequestOffers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RequestOffers.Include(r => r.Category).Include(r => r.Creator).Include(r => r.Tour).Where(o => o.Active && (o.State == 0 || o.State == 1));
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> CompletedRequests()
        {
            var applicationDbContext = _context.RequestOffers.Include(r => r.Category).Include(r => r.Creator).Include(r => r.Tour).Where(o => o.Active && (o.State == 2));
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> ChangeState(int id)
        {
            var requestOffer = await _context.RequestOffers.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (requestOffer == null)
            {
                return NotFound();
            }
            requestOffer.State = 2;
            _context.Update(requestOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DisActive(int id)
        {
            var requestOffer = await _context.RequestOffers.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (requestOffer == null)
            {
                return NotFound();
            }
            requestOffer.Active = false;
            _context.Update(requestOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: RequestOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestOffer = await _context.RequestOffers
                .Include(r => r.Category)
                .Include(r => r.Creator)
                .Include(r => r.Tour)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestOffer == null)
            {
                return NotFound();
            }

            return View(requestOffer);
        }
        public async Task<IActionResult> SendMail(string message, string subject, string email, int RequestId)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(email, subject, GetMail(message, RequestId));
            return Json(true);
        }
        private string GetMail(string message, int id)
        {
            #region html
            var html = @"<!DOCTYPE ><html><head><meta charset='UTF-8'><meta content='width=device-width, initial-scale=1' name='viewport'><meta name='x-apple-disable-message-reformatting'><meta http-equiv='X-UA-Compatible' content='IE=edge'><meta content='telephone=no' name='format-detection'><title></title><style type='text/css'>a {text-decoration: none;}</style><link href='https://fonts.googleapis.com/css?family=Open Sans:400,400i,700,700i' rel='stylesheet'><style>#outlook a {padding: 0;}.ExternalClass {width: 100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {line-height: 100%;}.es-button {mso-style-priority: 100 !important;text-decoration: none !important;}a[x-apple-data-detectors] {color: inherit !important;text-decoration: none !important;font-size: inherit !important;font-family: inherit !important;font-weight: inherit !important;line-height: inherit !important;}.es-desk-hidden {display: none;float: left;overflow: hidden;width: 0;max-height: 0;line-height: 0;mso-hide: all;}html,body {width: 100%;font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;}table {mso-table-lspace: 0pt;mso-table-rspace: 0pt;border-collapse: collapse;border-spacing: 0px;} table td,html,body,.es-wrapper {padding: 0;Margin: 0;}.es-content,.es-header,.es-footer {table-layout: fixed !important;width: 100%;} img {display: block;border: 0;outline: none;text-decoration: none;-ms-interpolation-mode: bicubic;}table tr {border-collapse: collapse;} p,hr {Margin: 0;} h1,h2,h3,h4,h5 {Margin: 0;line-height: 120%;mso-line-height-rule: exactly;font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;}p,ul li,ol li,a {-webkit-text-size-adjust: none;-ms-text-size-adjust: none;mso-line-height-rule: exactly;} .es-left {float: left;} .es-right {float: right;}.es-p5 {padding: 5px;} .es-p5t {padding-top: 5px;}.es-p5b {padding-bottom: 5px;} .es-p5l { padding-left: 5px;} .es-p5r {padding-right: 5px;} .es-p10 {padding: 10px;}.es-p10t {padding-top: 10px;} .es-p10b {padding-bottom: 10px;} .es-p10l {padding-left: 10px;} .es-p10r {padding-right: 10px;} .es-p15 {padding: 15px;} .es-p15t {padding-top: 15px;}.es-p15b {padding-bottom: 15px;} .es-p15l {padding-left: 15px;} .es-p15r {padding-right: 15px;} .es-p20 {padding: 20px;} .es-p20t {padding-top: 20px;} .es-p20b {padding-bottom: 20px;}es-p20l {padding-left: 20px;} .es-p20r {padding-right: 20px;} .es-p25 {padding: 25px;}.es-p25t {padding-top: 25px;}.es-p25b {padding-bottom: 25px;}.es-p25l {padding-left: 25px;}.es-p25r {padding-right: 25px;}.es-p30 {padding: 30px;}.es-p30t {padding-top: 30px;}.es-p30b {padding-bottom: 30px;}.es-p30l {padding-left: 30px;}.es-p30r {padding-right: 30px;}.es-p35 {padding: 35px;}.es-p35t {padding-top: 35px;}.es-p35b {padding-bottom: 35px;}.es-p35l {padding-left: 35px;}.es-p35r {padding-right: 35px;}.es-p40 {padding: 40px;}.es-p40t {padding-top: 40px;}.es-p40b {padding-bottom: 40px;}.es-p40l {padding-left: 40px;}.es-p40r {padding-right: 40px;}.es-menu td {border: 0;}.es-menu td a img {display: inline !important;}a {font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;font-size: 16px;text-decoration: none;} h1 {font-size: 36px;font-style: normal;font-weight: bold;color: #333333;}h1 a {font-size: 36px;} h2 { font-size: 30px;font-style: normal;font-weight: bold;color: #333333;} h2 a {font-size: 30px;} h3 { font-size: 18px;font-style: normal;font-weight: normal;color: #333333;} h3 a { font-size: 18px;}p,ul li,ol li {font-size: 16px;font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;line-height: 150%;}ul li,ol li {Margin-bottom: 15px;}.es-menu td a {text-decoration: none;display: block;}.es-wrapper {width: 100%;height: 100%;background-repeat: repeat;background-position: center top;}.es-wrapper-color {background-color: #eeeeee;}.es-content-body {background-color: #ffffff;}.es-content-body p,.es-content-body ul li,.es-content-body ol li {color: #333333;}.es-content-body a {color: #ed8e20;}.es-header {background-color: transparent; background-repeat: repeat;background-position: center top;}.es-header-body {background-color: #044767;}.es-header-body p,.es-header-body ul li,.es-header-body ol li {color: #ffffff;font-size: 14px;}.es-header-body a {color: #ffffff;font-size: 14px;}.es-footer {background-color: transparent;background-repeat: repeat;background-position: center top;}.es-footer-body {background-color: #ffffff;}.es-footer-body p,.es-footer-body ul li,.es-footer-body ol li {color: #333333;font-size: 14px;}.es-footer-body a { color: #333333;font-size: 14px;}.es-infoblock,.es-infoblock p,.es-infoblock ul li,.es-infoblock ol li {line-height: 120%;font-size: 12px;color: #cccccc;}.es-infoblock a {font-size: 12px;color: #cccccc;}a.es-button {border-style: solid;border-color: #66b3b7;border-width: 15px 30px 15px 30px;display: inline-block;background: #66b3b7;border-radius: 5px;font-size: 18px;font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;font-weight: normal;font-style: normal;line-height: 120%;color: #ffffff;text-decoration: none;width: auto;text-align: center;}.es-button-border {border-style: solid solid solid solid;border-color: transparent transparent transparent transparent;background: #66b3b7;border-width: 0px 0px 0px 0px;display: inline-block;border-radius: 5px;width: auto;}@media only screen and (max-width: 600px) {p,ul li,ol li,a {font-size: 16px !important;line-height: 150% !important;}h1 {font-size: 32px !important;text-align: center;line-height: 120% !important;}h2 {font-size: 26px !important;text-align: center;line-height: 120% !important;}h3 {font-size: 20px !important;text-align: center;line-height: 120% !important;}h1 a {font-size: 32px !important;}h2 a {font-size: 26px !important; }h3 a {font-size: 20px !important;}.es-menu td a {font-size: 16px !important;}.es-header-body p,.es-header-body ul li,.es-header-body ol li,.es-header-body a {font-size: 16px !important;}.es-footer-body p,.es-footer-body ul li,.es-footer-body ol li,.es-footer-body a {font-size: 16px !important;}.es-infoblock p,.es-infoblock ul li,.es-infoblock ol li, .es-infoblock a {font-size: 12px !important;}*[class='gmail-fix'] {display: none !important;} .es-m-txt-c, .es-m-txt-c h1,.es-m-txt-c h2,.es-m-txt-c h3 {text-align: center !important;}.es-m-txt-r,.es-m-txt-r h1,.es-m-txt-r h2,.es-m-txt-r h3 {text-align: right !important;}.es-m-txt-l,.es-m-txt-l h1,.es-m-txt-l h2,.es-m-txt-l h3 {text-align: left !important;}.es-m-txt-r img,.es-m-txt-c img,.es-m-txt-l img {display: inline !important;}.es-button-border {display: inline-block !important;}a.es-button {font-size: 16px !important;display: inline-block !important;border-width: 15px 30px 15px 30px !important;}.es-btn-fw {border-width: 10px 0px !important;text-align: center !important;}.es-adaptive table,.es-btn-fw,.es-btn-fw-brdr,.es-left,.es-right {width: 100% !important;}.es-content table,.es-header table,.es-footer table, .es-content,.es-footer,.es-header {width: 100% !important;max-width: 600px !important;}.es-adapt-td {display: block !important;width: 100% !important;}.adapt-img {width: 100% !important;height: auto !important;}.es-m-p0 {padding: 0px !important; }.es-m-p0r {padding-right: 0px !important;}.es-m-p0l {padding-left: 0px !important;}.es-m-p0t { padding-top: 0px !important;}.es-m-p0b {padding-bottom: 0 !important;}.es-m-p20b {padding-bottom: 20px !important;}.es-mobile-hidden,.es-hidden {display: none !important;}.es-desk-hidden {display: table-row!important;width: auto!important;overflow: visible!important;float: none!important;max-height: inherit!important;line-height: inherit!important;}.es-desk-menu-hidden {display: table-cell!important;}table.es-table-not-adapt,.esd-block-html table {width: auto !important;}table.es-social {display: inline-block !important;}table.es-social td {display: inline-block !important;}}.es-p-default {padding-top: 20px;padding-right: 35px;padding-bottom: 0px;padding-left: 35px;}.es-p-all-default {padding: 0px;}</style></head>
<body><div class='es-wrapper-color'><table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-email-paddings' valign='top'><table class='es-content esd-header-popover' cellspacing='0' cellpadding='0' align='center'><tbody><tr> </tr><tr><td class='esd-stripe' esd-custom-block-id='7681' align='center'><table class='es-header-body' style='background-color: rgb(4, 71, 103);' width='600' cellspacing='0' cellpadding='0' bgcolor='#044767' align='center'><tbody><tr><td class='esd-structure es-p35t es-p35b es-p35r es-p35l' align='left'><!--[if mso]><table width='530' cellpadding='0' cellspacing='0'><tr><td width='340' valign='top'>
<table class='es-left' cellspacing='0' cellpadding='0' align='left'><tbody><tr><td class='es-m-p0r es-m-p20b esd-container-frame' width='340' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-block-text es-m-txt-c' align='left'><h1 style='color: #ffffff; line-height: 100%;'>alas de Isis</h1></td></tr></tbody>
</table></td></tr></tbody></table> </td></tr></tbody></table></td></tr></tbody></table><table class='es-content' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td class='esd-stripe' align='center'><table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'><tbody><tr><td class='esd-structure es-p40t es-p35r es-p35l' align='left'><table width='100%' cellspacing='0' cellpadding='0'>
<tbody><tr><td class='esd-container-frame' width='530' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody> <tr><td class='esd-block-text es-p10b' align='center'><h2>¡Gracias por tu solicitud!</h2></td></tr><tr><td class='esd-block-text es-p15t es-p20b' align='left'><p style='font-size: 16px; color: #777777;'>
{Message}</p></td><td><p>Fecha : {Date}</p></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>
<table class='es-content' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td class='esd-stripe' align='center'><table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'><tbody><tr><td class='esd-structure es-p20t es-p35r es-p35l' align='left'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-container-frame' width='530' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-block-text es-p10t es-p10b es-p10r es-p10l' bgcolor='#eeeeee' align='left'>
<table style='width: 500px;' class='cke_show_border' cellspacing='1' cellpadding='1' border='0' align='left'><tbody><tr><td width='80%'><h4>número de solicitud#</h4></td><td width='20%'><h4>{RequestId}</h4></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td class='esd-structure es-p35r es-p35l' align='left'><table width='100%' cellspacing='0' cellpadding='0'><tbody>
<tr><td class='esd-container-frame' width='530' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-block-text es-p10t es-p10b es-p10r es-p10l' align='left'><table style='width: 500px;' class='cke_show_border' cellspacing='1' cellpadding='1' border='0' align='left'><tbody>
<tr><td style='padding: 5px 10px 5px 0' width='80%' align='left'>
<p>adulto {AdultQuantity}</p>
</td><td style='padding: 5px 0' width='20%' align='left'>
<p>{AdultVal}</p>
</td></tr><tr><td style='padding: 5px 10px 5px 0' width='80%' align='left'>
<p>Bebé ({BabyQuantity})</p>
</td><td style='padding: 5px 0' width='20%' align='left'>
<p>{BabyVal}</p>
</td></tr><tr><td style='padding: 5px 10px 5px 0' width='80%' align='left'>
<p>Habitación extra ({ExtraRoomQuantity})</p>
</td><td style='padding: 5px 0' width='20%' align='left'>
<p>{ExtraRoomVal}</p>
</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td class='esd-structure es-p10t es-p35r es-p35l' align='left'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-container-frame' width='530' valign='top' align='center'><table style='border-top: 3px solid rgb(238, 238, 238); border-bottom: 3px solid rgb(238, 238, 238);' width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-block-text es-p15t es-p15b es-p10r es-p10l' align='left'><table style='width: 500px;' class='cke_show_border' cellspacing='1' cellpadding='1' border='0' align='left'><tbody><tr><td width='80%'><h4>TOTAL</h4></td><td width='20%'>
<h4>{TotalVal}</h4>
</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>
<table class='es-content' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td class='esd-stripe' esd-custom-block-id='7797' align='center'><table class='es-content-body' style='background-color: rgb(27, 155, 163);' width='600' cellspacing='0' cellpadding='0' bgcolor='#1b9ba3' align='center'>
<tbody><tr><td class='esd-structure es-p35t es-p35b es-p35r es-p35l' align='left'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-container-frame' width='530' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td align='left' class='esd-block-text'>
<p>{ContactInfo}</p>
</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table> <table class='esd-footer-popover es-content' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td class='esd-stripe' align='center'><table class='es-content-body' style='background-color: transparent;' width='600' cellspacing='0' cellpadding='0' align='center'><tbody><tr><td class='esd-structure es-p30t es-p30b es-p20r es-p20l' align='left'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td class='esd-container-frame' width='560' valign='top' align='center'><table width='100%' cellspacing='0' cellpadding='0'><tbody><tr><td align='center' class='esd-empty-container' style='display: none;'></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div></body></html>";
            #endregion
            var request = _context.RequestOffers.Include(c => c.Creator).FirstOrDefault(c => c.Id == id);
            var user = _context.Users.FirstOrDefault(u => u.Id == request.CreatorId);
            var ContactInfo = @"Address : 675 Massachusetts Avenue Cambridge, MA 02139 <br /> Phone:+6997897878";
            var modifiedHtml = html.Replace("{Message}", message).Replace("{RequestId}", request.Id.ToString())
                .Replace("{AdultQuantity}", request.AdultQuantity.ToString())
                .Replace("{AdultVal}", request.TotalAdult.ToString())
                .Replace("{BabyQuantity}", request.BabyQuantity.ToString())
                .Replace("{BabyVal}", request.TotalBaby.ToString())
                .Replace("{ExtraRoomQuantity}", request.ExtraRoomQuantity.ToString())
                .Replace("{ExtraRoomVal}", request.TotalExtraRoom.ToString())
                .Replace("{TotalVal}", request.Total.ToString())
                .Replace("{Date}", request.Date.ToString("dd-mm-yyyy"))
                .Replace("{ContactInfo}", ContactInfo);
            return modifiedHtml;
        }
        private bool RequestOfferExists(int id)
        {
            return _context.RequestOffers.Any(e => e.Id == id);
        }
    }
}
