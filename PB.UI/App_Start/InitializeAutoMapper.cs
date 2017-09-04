using PB.UI.Models;

namespace PB.UI.App_Start
{
    public static class InitializeAutoMapper
    {
        public static void Initialize()
        {
            CreateAll();
        }

        private static void CreateAll()
        {

            #region BILL

            #region Bill View Models
            AutoMapper.Mapper.CreateMap<BillViewModel, Entities.Bill>();
            AutoMapper.Mapper.CreateMap<Entities.Bill, BillViewModel>()
                    .ForMember(x => x.DueDaySuffix, opt => opt.Ignore())
                    .ForMember(x => x.Paycheck, opt => opt.UseValue(null))
                    .ForMember(x => x.CustomBillCount, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<CreateBillViewModel, Entities.Bill>()
                    //.ForMember(x => x.Glyphicon, opt => opt.Ignore())
                    ;
      AutoMapper.Mapper.CreateMap<Entities.Bill, CreateBillViewModel>()
                    .ForMember(x => x.DueDaySuffix, opt => opt.Ignore())
                    .ForMember(x => x.DOTM, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Entities.Bill, EditBillViewModel>()
                    .ForMember(x => x.CustomBills, opt => opt.Ignore())
                    .ForMember(x => x.Bill, opt => opt.Ignore())
                    .ForMember(x => x.DOTM, opt => opt.Ignore());

            AutoMapper.Mapper.CreateMap<Entities.CustomBill, CustomBillViewModel>()
                    .ForMember(x => x.Exists, x => x.UseValue(true));
            AutoMapper.Mapper.CreateMap<CustomBillViewModel, Entities.CustomBill>();

            #endregion
            #endregion

            #region INCOME
            AutoMapper.Mapper.CreateMap<Entities.Paycheck, PaycheckViewModel>()
                    .ForMember(x => x.PayCheck, opt => opt.UseValue(null))
                    .ForMember(x => x.Exists, opt => opt.Ignore());
            #endregion

            #region SETTINGS
            AutoMapper.Mapper.CreateMap<Entities.Setting, SettingsViewModel>()
                    .ForMember(x => x.PaycheckAmount, opt => opt.Ignore());
            #endregion

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}