using OBS.MVC.Models.ViewModels;
using System;
using System.Web.Mvc;

namespace OBS.MVC.ModelBinders
{
    public class OgrenciDersNotuModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var istek = controllerContext.HttpContext.Request;

            int id, vize1, vize2, final;

            string sid = istek.Form.Get("Id");
            if (string.IsNullOrEmpty(sid))
                id = 0;
            else
                id = Convert.ToInt32(sid);

            string svize1 = istek.Form.Get("Vize1");
            if (string.IsNullOrEmpty(svize1))
                vize1 = 0;
            else
                vize1 = Convert.ToInt32(svize1);

            string svize2 = istek.Form.Get("Vize2");
            if (string.IsNullOrEmpty(svize2))
                vize2 = 0;
            else
                vize2 = Convert.ToInt32(svize2);

            string sfinal = istek.Form.Get("Final");
            if (string.IsNullOrEmpty(sfinal))
                final = 0;
            else
                final = Convert.ToInt32(sfinal);

            int ortalama = Convert.ToInt32(((vize1 + vize2) * 0.3) + final * 0.4);
            return new NOgrenciDersNotuKayit
            {
                Id = id,
                Vize1 = vize1,
                Vize2 = vize2,
                Final = final,
                Ortalama = ortalama
            };
        }
    }
}