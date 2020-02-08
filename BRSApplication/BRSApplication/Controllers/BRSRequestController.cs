using BRSApplication.Models;
using BRSApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BRSApplication.Controllers
{
    public class BRSRequestController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public BRSRequestViewModel viewmodel { get; set; }

        public AuthenticationRoleClass Authrole { get; set; }
        public BRSRequestController()
        {
            _context = new ApplicationDbContext();
            Authrole = new AuthenticationRoleClass();

        }

    
        public ActionResult Index(string name)
        {
            if (viewmodel == null)
                LoadData();
            if (!string.IsNullOrEmpty(name))
            {
               var AuthInfo= Authrole.RoleInfo.FirstOrDefault(x=>x.Value==name);
                viewmodel.userInfo = new UserInfo() { RoleType = AuthInfo.Key, Username = AuthInfo.Value };
                ViewBag.UserName = AuthInfo.Value;
                ViewBag.Role = AuthInfo.Key;
            }
          
            return View(viewmodel);
            
        }

        public IEnumerable<BRSRequest> GetBRSList()
        {
            List<BRSRequest> dataList = new List<BRSRequest>();
            foreach (var item in _context.BrsRequest.ToList())
            {
                var tempItem = item;
                if (item.GeosId.Contains(','))
                {
                    foreach (var eachGeo in item.GeosId.Split(','))
                    {

                        BRSRequest bRSRequest = new BRSRequest()
                        {
                            EnvironmentId = tempItem.EnvironmentId,
                            SlotId = tempItem.SlotId,
                            BuildId = tempItem.BuildId,
                            FeatureAreaId = tempItem.FeatureAreaId,
                            FeatureNameId = tempItem.FeatureNameId,
                            SettingId = tempItem.SettingId,
                            ExpiryDate = tempItem.ExpiryDate, //DateTime.Now; //;
                            Comments = tempItem.Comments,
                            Value = tempItem.Value,
                            GeosId = eachGeo,
                            Id = tempItem.Id,
                            FeatureArea=tempItem.FeatureArea,
                            FeatureName=tempItem.FeatureName,
                            Setting=tempItem.Setting,
                            Environment=tempItem.Environment,
                            Build=tempItem.Build

                        };                        
                        dataList.Add(bRSRequest);
                    }
                }
                else
                {
                    dataList.Add(tempItem);
                }
            }
            return dataList;
            // _context.BrsRequest.ToList();
        }
        public void LoadData()
        {
            viewmodel = new BRSRequestViewModel();
            viewmodel.Environments = _context.Environment.ToList();
            viewmodel.BRSRequestDetails = GetBRSList();// _context.BrsRequest.ToList();
            viewmodel.EnvGeoMap = _context.EnvGeoMap.ToList();
            viewmodel.Slots = _context.Slot.ToList();
            viewmodel.Builds = _context.Build.ToList();
            viewmodel.Builds.Add(new Build { Id = -1, BuildNumber = "Other" });
            viewmodel.FeatureAreas = _context.FeatureArea.ToList();
            viewmodel.FeatureNames = _context.FeatureName.ToList();
            viewmodel.Settings = _context.Setting.ToList();
            viewmodel.Settings.Add(new Setting { Id = -1, Name = "Other" });

            //viewmodel.GeosForFilter = _context.Geos.ToList();
            // viewmodel.GeosForProds = _context.Geos.ToList();

            viewmodel.BRSRequestDetails = _context.BrsRequest.ToList();
            var Enids = viewmodel.BRSRequestDetails.Where(x => x.EnvironmentId > 0)
                                  .Select(p => p.EnvironmentId)
                                  .Distinct();

            foreach (var item in Enids)
            {
                viewmodel.EnvsForFilter.Add(viewmodel.Environments.Where(x => x.Id == item).FirstOrDefault());
            }
            viewmodel.EnvIdForFilter = 3;
             viewmodel.GeosForFilter= GetGeosByListEnvId(3);
           
            
        }
        // GET: BRSRequest
        // int EnvIdForFilter,int GeoIdsForFilter,int BuildIdsForFilter,
        [HttpPost]
        public ActionResult UpdateBRSTable( BRSRequestViewModel envVM, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Index",envVM);
            //}
            string GoIds = Convert.ToString(envVM.GeoIdsForFilter);
            if (envVM.GeoIdsForFilter.Length==1 && envVM.GeoIdsForFilter[0] == 0)
            {
                var lst = _context.EnvGeoMap.Where(x => x.EnvironmentId == envVM.EnvIdForFilter);
                GoIds = "";
                foreach (var item in lst)
                {
                    GoIds = GoIds + item.GeoId + ',';
                }
                GoIds = GoIds.TrimEnd(',');
            }
            LoadData();
            //viewmodel.BRSRequestDetails = _context.BrsRequest.ToList();
            viewmodel.BRSRequestDetails = viewmodel.BRSRequestDetails.Where(x => x.EnvironmentId == envVM.EnvIdForFilter && x.GeosId == GoIds && x.BuildId == envVM.BuildIdsForFilter).ToList();
            
            List<int> _geoProds = _context.EnvGeoMap.Where(x => x.EnvironmentId == envVM.EnvIdForFilter).Select(x => x.GeoId).ToList();
           
            Geos geos = new Geos();
            geos.GeoName = "Select All";
            geos.Id = 0;
            List<Geos> geos1 = new List<Geos>();
            geos1.Add(geos);
            var geoProdInfo =
                _context.Geos
                               .Where(t => _geoProds.Contains(t.Id));
            geos1.AddRange(geoProdInfo);
            //foreach (var item in geoProdInfo)
            //{
            //    GeoNames.Add(new SelectListItem { Text = item.GeoName, Value = item.Id.ToString() });

            //}
            viewmodel.GeosForFilter = geos1;
            
            viewmodel.BuildIdsForFilter = envVM.BuildIdsForFilter;
            viewmodel.GeoIdsForFilter = envVM.GeoIdsForFilter;
            viewmodel.EnvIdForFilter = envVM.EnvIdForFilter;

            return View("Index", viewmodel);
        }

        [HttpPost]
        public JsonResult GetGeos(int EnvId)
        { 
            return Json(GetGeosByEnvId(EnvId), JsonRequestBehavior.AllowGet);
           
        }

        public List<SelectListItem> GetGeosByEnvId(int EnvId)
        {
            List<SelectListItem> GeoNames = new List<SelectListItem>();
            List<int> _geoProds = _context.EnvGeoMap.Where(x => x.EnvironmentId == EnvId).Select(x => x.GeoId).ToList();
            if (_geoProds.Count > 0)
            {
                GeoNames.Add(new SelectListItem { Text = "Select All", Value = "0" });

            }
            var geoProdInfo = _context.Geos
                               .Where(t => _geoProds.Contains(t.Id));

            foreach (var item in geoProdInfo)
            {
                GeoNames.Add(new SelectListItem { Text = item.GeoName, Value = item.Id.ToString() });

            }
            return GeoNames;
        }

        public List<Geos> GetGeosByListEnvId(int EnvId)
        {
           
            List<int> _geoProds = _context.EnvGeoMap.Where(x => x.EnvironmentId == EnvId).Select(x => x.GeoId).ToList();

            Geos geos = new Geos();
            geos.GeoName = "Select All";
            geos.Id = 0;
            List<Geos> geos1 = new List<Geos>();
            geos1.Add(geos);
            var geoProdInfo =
                _context.Geos
                               .Where(t => _geoProds.Contains(t.Id));
            geos1.AddRange(geoProdInfo);
            return geos1;
        }

        [HttpPost]
        public JsonResult GETBRSById(int Id)
        {

            var test = _context.BrsRequest.Where(x=>x.Id==Id).FirstOrDefault();
            var FArea = test.FeatureArea.FeatureArea;
            var FName = test.FeatureName.FeatureName;
            var Setting = test.Setting.Name;
            var Value = test.Value;
            var output= new { FArea, FName, Setting , Value};
            //List<SelectListItem> GeoNames = new List<SelectListItem>();


            //List<int> _geoProds = _context.EnvGeoMap.Where(x => x.EnvironmentId == EnvId).Select(x => x.GeoId).ToList();
            //if (_geoProds.Count > 0)
            //{
            //    GeoNames.Add(new SelectListItem { Text = "Select All", Value = "0" });

            //}
            //var geoProdInfo = _context.Geos
            //                   .Where(t => _geoProds.Contains(t.Id));

            //foreach (var item in geoProdInfo)
            //{
            //    GeoNames.Add(new SelectListItem { Text = item.GeoName, Value = item.Id.ToString() });

            //}

            return Json(output, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult Submit(BRSRequestViewModel envVM)
        {
            if (!ModelState.IsValid)
            {
                return View(envVM);
            }

           
            envVM.BRSRequest = new BRSRequest();//Initializing the BRS Request Table
            var envName = _context.Environment.Where(x => x.Id == envVM.EnvId).FirstOrDefault();
            envVM.BRSRequest.EnvironmentId = envName.Id;
            envVM.BRSRequest.SlotId = _context.Slot.Where(x => x.Id == envVM.SlotId).FirstOrDefault().Id;
            envVM.BRSRequest.BuildId = _context.Build.Where(x => x.Id == envVM.BuildId).FirstOrDefault().Id;
            envVM.BRSRequest.FeatureAreaId = _context.FeatureArea.Where(x => x.Id == envVM.FeatureAreaId).FirstOrDefault().Id;
            envVM.BRSRequest.FeatureNameId = _context.FeatureName.Where(x => x.Id == envVM.FeatureNameId).FirstOrDefault().Id;
             envVM.BRSRequest.SettingId = _context.Setting.Where(x => x.Id == envVM.SettingId).FirstOrDefault().Id;
            envVM.BRSRequest.ExpiryDate = envVM.ExpiryDateValue; //DateTime.Now; //;
            envVM.BRSRequest.Comments = envVM.Comments;
            envVM.BRSRequest.Value = envVM.Value;

            if (envVM.GeoIds.Contains(0))      
            {
                var lst = _context.EnvGeoMap.Where(x => x.EnvironmentId == envVM.EnvId);
                string Geonames = "";
                foreach (var item in lst)
                {
                    Geonames = Geonames + item.GeoId + ',';
                }
                Geonames = Geonames.TrimEnd(',');
                envVM.BRSRequest.GeosId = Geonames;
            }
            else
            {
                string Geonames = "";
                foreach (var item in envVM.GeoIds)
                {
                    Geonames = Geonames + item.ToString() + ','; //_context.EnvGeoMap.Find(item).GeoId + ',';
                }
                Geonames = Geonames.TrimEnd(',');
                envVM.BRSRequest.GeosId = Geonames;
            }
            if (envVM.BRSRequestEditId != 0)
            {
                var bRSRequest = _context.BrsRequest.Where(x => x.Id == envVM.BRSRequestEditId).FirstOrDefault();
                bRSRequest.ApproveDate = envVM.BRSRequest.ApproveDate;
                bRSRequest.ApproverAlias = envVM.BRSRequest.ApproverAlias;
                bRSRequest.EnvironmentId = envVM.BRSRequest.EnvironmentId;
                bRSRequest.ExpiryDate = envVM.BRSRequest.ExpiryDate;
                bRSRequest.BuildId = envVM.BRSRequest.BuildId;
                bRSRequest.Comments = envVM.BRSRequest.Comments;
                bRSRequest.FeatureAreaId = envVM.BRSRequest.FeatureAreaId;
                bRSRequest.SettingId = envVM.BRSRequest.SettingId;
                bRSRequest.RequestedDate = envVM.BRSRequest.RequestedDate;
                bRSRequest.GeosId = envVM.BRSRequest.GeosId;
                bRSRequest.SlotId = envVM.BRSRequest.SlotId;
            }
            else
            {
                _context.BrsRequest.Add(envVM.BRSRequest);
            }
            _context.SaveChanges();
            LoadData();
            ViewBag.Message = "Saved Successfully";
            ModelState.Clear();
            return View("Index",viewmodel);
        }

        // GET: BRSRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BRSRequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BRSRequest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BRSRequest/Edit/5
      
        public ActionResult Edit(int id)
        {
            
            viewmodel = new BRSRequestViewModel();
            viewmodel.Environments = _context.Environment.ToList();
            viewmodel.BRSRequestDetails = _context.BrsRequest.ToList();
            viewmodel.EnvGeoMap = _context.EnvGeoMap.ToList();
            viewmodel.Slots = _context.Slot.ToList();
            viewmodel.Builds = _context.Build.ToList();
            viewmodel.Builds.Add(new Build { Id = -1, BuildNumber = "Other" });
            viewmodel.FeatureAreas = _context.FeatureArea.ToList();
            viewmodel.FeatureNames = _context.FeatureName.ToList();
            viewmodel.Settings = _context.Setting.ToList();
            viewmodel.Settings.Add(new Setting { Id = -1, Name = "Other" });

            //viewmodel.GeosForFilter = _context.Geos.ToList();
            // viewmodel.GeosForProds = _context.Geos.ToList();

            viewmodel.BRSRequestDetails = _context.BrsRequest.ToList();
            var Enids = viewmodel.BRSRequestDetails.Where(x => x.EnvironmentId > 0)
                                  .Select(p => p.EnvironmentId)
                                  .Distinct();
            var test = _context.BrsRequest.Where(x => x.Id == id).FirstOrDefault();
            viewmodel.SettingId = test.Setting.Id;
            viewmodel.FeatureAreaId = test.FeatureAreaId;
            viewmodel.FeatureNameId = test.FeatureNameId;

            viewmodel.EnvId = test.EnvironmentId;
            List<SelectListItem> GeoNames = new List<SelectListItem>();
            List<int> _geoProds = _context.EnvGeoMap.Where(x => x.EnvironmentId == viewmodel.EnvId).Select(x => x.GeoId).ToList();
            if (_geoProds.Count > 0)
            {
                GeoNames.Add(new SelectListItem { Text = "Select All", Value = "0" });

            }
            Geos geos = new Geos();
            geos.GeoName= "Select All";
            geos.Id = 0;
            List<Geos> geos1 = new List<Geos>();
            geos1.Add(geos);
            var geoProdInfo=
                _context.Geos
                               .Where(t => _geoProds.Contains(t.Id));
            geos1.AddRange(geoProdInfo);
            //foreach (var item in geoProdInfo)
            //{
            //    GeoNames.Add(new SelectListItem { Text = item.GeoName, Value = item.Id.ToString() });

            //}
            viewmodel.GeosForProds = geos1;
            var GeosArray = test.GeosId.Split(',');
            viewmodel.GeoIds = new int[GeosArray.Length];
            for (int item=0;item< GeosArray.Length-1;item++)
            {
                viewmodel.GeoIds[item] =Convert.ToInt32(GeosArray[item]);
            }
            viewmodel.SlotId = test.SlotId;
            viewmodel.BuildId = test.BuildId;
            viewmodel.BuildName = test.Build.BuildNumber;
            viewmodel.ExpiryDateValue = test.ExpiryDate;
            viewmodel.Comments = test.Comments;
            foreach (var item in Enids)
            {
                viewmodel.EnvsForFilter.Add(viewmodel.Environments.Where(x => x.Id == item).FirstOrDefault());
            }
            viewmodel.BRSRequestEditId = id;
            viewmodel.EnvIdForFilter = 0;
            viewmodel.GeosForFilter = GetGeosByListEnvId(3);
            ViewBag.isInEditMode = "true";
            return View("Index",viewmodel);
        }

        // POST: BRSRequest/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: BRSRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, int GeoID, int? EnvId = -1, int? GeosId = -1, int? BuildID = -1)
        {
            var bRSRequest = _context.BrsRequest.Where(x => x.Id == Id).FirstOrDefault();
            if (bRSRequest.GeosId.Contains(','))
            {
                string geoId = GeoID.ToString();
                bRSRequest.GeosId= bRSRequest.GeosId.Replace(geoId, "").Replace(",,", ",").TrimEnd(',');
              
            }
            else
            {
                _context.BrsRequest.Remove(bRSRequest);
            }
            _context.SaveChanges();
            viewmodel = new BRSRequestViewModel();
            string Geonames = "";
            if (GeosId == 0)
            {
                var lst = _context.EnvGeoMap.Where(x => x.EnvironmentId == EnvId);

                foreach (var item in lst)
                {
                    Geonames = Geonames + item.GeoId + ',';
                }
                Geonames = Geonames.TrimEnd(',');
            }
            else
            {
                Geonames = GeosId.ToString();
            }

            if (EnvId > 0 && BuildID > 0 && GeosId >= 0)
            {
                viewmodel.BRSRequestDetails = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == EnvId && x.GeosId == Geonames && x.BuildId == BuildID).ToList();
            }
            else if (EnvId > 0 && GeosId >= 0)
                viewmodel.BRSRequestDetails = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == EnvId && x.GeosId == Geonames).ToList();
            else if (EnvId > 0)
                viewmodel.BRSRequestDetails = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == EnvId).ToList();
            else
                viewmodel.BRSRequestDetails = _context.BrsRequest.ToList();

            return PartialView("BRSTableView", viewmodel.BRSRequestDetails);
        }


        public ActionResult LoadBRSValues(int[] GeosId, int EnvId = -1, int? BuildID = -1,string Name="",string Role="")
        {
            viewmodel = new BRSRequestViewModel();
            string Geonames = "";
            List<string> lst=new List<string>();
            if (GeosId.Length == 1 && GeosId[0]==0)
            {
                lst = _context.EnvGeoMap.Where(x => x.EnvironmentId == EnvId).Select(x=>x.GeoId.ToString()).ToList();

                foreach (var item in lst)
                {
                    Geonames = Geonames + item + ',';
                }
                Geonames = Geonames.TrimEnd(',');
            }
            else
            {
                foreach (var item in GeosId)
                {
                    Geonames = Geonames + item + ',';
                }
                Geonames = Geonames.TrimEnd(',');
              
            }
            if (BuildID > 0 && GeosId.Length == 1 && GeosId[0] != 0)
                viewmodel.BRSRequestDetails = GetBRSList().Where(x => x.EnvironmentId == EnvId && x.GeosId.Any(xx => Geonames.Contains(xx.ToString())) && x.BuildId == BuildID).ToList();
            else if (BuildID > 0)
                viewmodel.BRSRequestDetails = GetBRSList().Where(x => x.EnvironmentId == EnvId && x.GeosId.Any(xx => Geonames.Contains(xx.ToString())) && x.BuildId == BuildID).ToList();
            //else
            //    viewmodel.BRSRequestDetails = GetBRSList().ToList().Where(x => x.EnvironmentId == EnvId && x.GeosId.Any(xx => lst.Contains(xx.ToString()))).ToList();

            if (!string.IsNullOrEmpty(Name))
            {

               
                ViewBag.UserName = Name;
                ViewBag.Role = Role;
                return PartialView("BRSTableView", viewmodel.BRSRequestDetails);
                //viewmodel.userInfo = new UserInfo() { RoleType = AuthInfo.Key, Username = AuthInfo.Value };

            }
            return PartialView("BRSTableView", viewmodel.BRSRequestDetails);

        }
        public ActionResult GetBRSApproveTableValues(string name,string role)
        {
            if (!string.IsNullOrEmpty(name))
            {

                var data2 = _context.BrsRequest.ToList();
                ViewBag.UserName = name;
                ViewBag.Role = role;
                return PartialView("BRSApproverView", data2);
                //viewmodel.userInfo = new UserInfo() { RoleType = AuthInfo.Key, Username = AuthInfo.Value };

            }
            var data = _context.BrsRequest.ToList();
            return PartialView("BRSApproverView", data);

        }

        


        public ActionResult GetBRSValues(int envID)
        {

            var data = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == envID).ToList();


            return Json(new { data = data }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetBRSTableValues(int envID)
        {

            var data = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == envID).ToList();


            return PartialView("BRSTableView", data);

        }

        public ActionResult GetloadBRSOWNPartial(string name,string role)
        {
            
          
                if(!string.IsNullOrEmpty(name))
                {
                                       
                    var data2 = _context.BrsRequest.Where(x=>x.RequestedAlias==name);
                    ViewBag.UserName = name;
                    ViewBag.Role = role;
                    return PartialView("BRSOWNRequests", data2);
                    //viewmodel.userInfo = new UserInfo() { RoleType = AuthInfo.Key, Username = AuthInfo.Value };
                    
                }
            
            var data = _context.BrsRequest.ToList();
            return PartialView("BRSOWNRequests", data);
        }

        [HttpPost]
        public ActionResult GetRegisterBRS(string RegisterBRSModel)
        {

            //var data = _context.BrsRequest.ToList().Where(x => x.EnvironmentId == envID).ToList();

            LoadData();
            return PartialView("registerBRS", viewmodel);

        }

        // POST: BRSRequest/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
