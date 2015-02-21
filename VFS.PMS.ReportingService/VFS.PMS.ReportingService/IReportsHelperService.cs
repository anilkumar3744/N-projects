using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VFS.PMS.ReportingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IReportsHelperService
    {
        [OperationContract]
        List<RegionEntity> GetAllRegions();

        [OperationContract]
        ////List<RegionEntity> GetRegions();
        RegionResponse GetRegions(RegionRequest request);

        [OperationContract]
        CompanyResponse GetCompaniesByRegionId(CompanyRequest companyRequest);

        [OperationContract]
        List<OrganizationUnitEntity> GetOrganizationUnits();

        [OperationContract]
        EligibleEmployeesResponse EligibleEmployees(EligibleEmployeesRequest eligibleEmployeRequest);

        [OperationContract]
        StatusResponse GetStatusReport(StatusRequest statusRequest);

        [OperationContract]
        EmployeeWiseResponse GetEmployeeWiseReport(EmployeeWiseRequest employeeRequest);

        [OperationContract]
        CompletionStatusResponse GetCompletionStatusReport(CompletionStatusRequest completionRequest);

        [OperationContract]
        RatingTrendResponse GetRatingTrendReport(RatingTrendRequest ratingRequest);

        [OperationContract]
        PDPResponse GetPDPReport(PDPRequest pdpRequest);

        [OperationContract]
        HRReviewResponse GetHRReviewReport(HRReviewRequest reviewRequest);

        [OperationContract]
        PIPResponse GetPIPReport(PIPRequest request);

        [OperationContract]
        List<PerformanceRatingEntity> GetPerformanceRatings();

        [OperationContract]
        List<PerformnceCycleEntity> GetAllPerformanceCycles();

        [OperationContract]
        RatingTrendResponse GetAllRatingTrendReport(AllRatingTrendRequest ratingRequest);

        #region AppraisalAllDetails
        [OperationContract]
        AppraisalDataResponse GetAppraisalData(AppraisalAllDataRequest appRequest);

        [OperationContract]
        GoalsResponse GetAppraiseeH1Goals(AppraisalAllDataRequest goalRequest);

        [OperationContract]
        GoalsResponse GetAppraiseeH2Goals(AppraisalAllDataRequest goalRequest);

        [OperationContract]
        CompetenciesResponse GetAppraiseeH1Competencies(AppraisalAllDataRequest competenyRequest);

        [OperationContract]
        CompetenciesResponse GetAppraiseeH2Competencies(AppraisalAllDataRequest competenyRequest);

        [OperationContract]
        DevelopmentMeasuresResponse GetAppraiseeH1DevelopmentMeasures(AppraisalAllDataRequest developmentMeasuresRequest);

        [OperationContract]
        DevelopmentMeasuresResponse GetAppraiseeH2DevelopmentMeasures(AppraisalAllDataRequest developmentMeasuresRequest);

        [OperationContract]
        AppraisalPIPResponse GetAppraiseeH1PIPData(AppraisalAllDataRequest pipRequest);

        [OperationContract]
        AppraisalPIPResponse GetAppraiseeH2PIPData(AppraisalAllDataRequest pipRequest);


        #endregion
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.

    [DataContract]
    public class ParametersEntity
    {
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public string RegionCode { get; set; }

        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public string OrganizationUnitCode { get; set; }
        [DataMember]
        public string OrganizationUnitText { get; set; }

        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string PositionText { get; set; }
        [DataMember]
        public string EmployeeGroupName { get; set; }
        [DataMember]
        public string EmployeeSubGroup { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string SubArea { get; set; }
        [DataMember]
        public string HRBusinessPatner { get; set; }
        [DataMember]//Added by Krishna
        public string HRBusinessPatnerCode { get; set; }
        [DataMember]
        public string AppraiserName { get; set; }
        [DataMember]
        public string AppraiserCode { get; set; }
        [DataMember]
        public string ReviewerName { get; set; }
        [DataMember]//Added by Krishna
        public string ReviewerCode { get; set; }
        [DataMember]
        public string JoiningDate { get; set; }
        [DataMember]
        public string ConfirmationDueDate { get; set; }
        [DataMember]
        public string ConfirmationDate { get; set; }
        [DataMember]
        public string EmployeeStatus { get; set; }
        [DataMember]
        public string EligibilityH1 { get; set; }
        [DataMember]
        public string EligibilityH2 { get; set; }
        [DataMember]
        public string OldEmployeeCode { get; set; }

        [DataMember]
        public string AppraisalCurrentState { get; set; }

        //added by jhansi for employee wise report

        [DataMember]
        public string H1Score { get; set; }
        [DataMember]
        public string H2Score { get; set; }
        [DataMember]
        public string FinalScore { get; set; }
        [DataMember]
        public string FinalRating { get; set; }
        [DataMember]
        public string WorkLevel { get; set; }
        [DataMember]
        public string Dummy { get; set; }
        [DataMember]
        public string Dummy1 { get; set; }

        [DataMember]
        public string H1PhaseEndDate { get; set; }
        [DataMember]
        public string H2PhaseEndDate { get; set; }
    }

    public class RegionEntity
    {
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public string RegionCode { get; set; }
    }

    public class CompanyEntity
    {
        [DataMember]
        public string CompanyName { get; set; }
        [DataMember]
        public string CompanyCode { get; set; }
    }

    public class OrganizationUnitEntity
    {
        [DataMember]
        public string OrganizationUnitLongName { get; set; }
    }

    public class CompletionStatusEntity
    {
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string OU { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string NoOfAppraisees { get; set; }
        [DataMember]
        public string NoOfAppraisalsCompleted { get; set; }
        [DataMember]
        public string NoOfAppraisalsPending { get; set; }
        [DataMember]
        public string PercentageOfCompletion { get; set; }
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string HRBusinessPartner { get; set; }
        [DataMember]
        public string HRBusinessPartnerCode { get; set; }
        [DataMember]
        public string Appraiser { get; set; }
        [DataMember]
        public string AppraiserCode { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        [DataMember]
        public string ReviewerCode { get; set; }
    }

    public class RatingTrendEntity
    {
        [DataMember]
        public string Rating { get; set; }
        [DataMember]
        public string Count { get; set; }
        [DataMember]
        public string Percentage { get; set; }
        [DataMember]
        public string RegionName { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string OU { get; set; }
        [DataMember]
        public string EmpCode { get; set; }
        [DataMember]
        public string HRBusinessPartner { get; set; }
        [DataMember]
        public string HRBusinessPartnerCode { get; set; }
        [DataMember]
        public string Appraiser { get; set; }
        [DataMember]
        public string AppraiserCode { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        [DataMember]
        public string ReviewerCode { get; set; }
        [DataMember]
        public double? Score { get; set; }
        [DataMember]
        public double TotalRecords { get; set; }
    }

    public class PDPEntity
    {
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string WorkLevel { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string SubArea { get; set; }
        [DataMember]
        public string OrganizationUnit { get; set; }
        [DataMember]
        public string HRBusinessPartner { get; set; }
        [DataMember]
        public string HRBusinessPartnerCode { get; set; }
        [DataMember]
        public string Appraiser { get; set; }
        [DataMember]
        public string AppraiserCode { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        [DataMember]
        public string ReviewerCode { get; set; }
        [DataMember]
        public string DMWhen { get; set; }
        [DataMember]
        public string DMWhat { get; set; }
        [DataMember]
        public string DMNextSteps { get; set; }
        [DataMember]
        public string H1AppraiseeComments { get; set; }
        [DataMember]
        public string H1AppraiserComments { get; set; }
        [DataMember]
        public string H2AppraiseeComments { get; set; }
        [DataMember]
        public string H2AppraiserComments { get; set; }
        [DataMember]
        public string OldEmployeeCode { get; set; }
        [DataMember]
        public string HRComments { get; set; }
        [DataMember]
        public string ReviewStatus { get; set; }
    }

    public class PIPEntity
    {
        [DataMember]
        public string EmployeeCode { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string WorkLevel { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string SubArea { get; set; }
        [DataMember]
        public string OrganizationUnit { get; set; }
        [DataMember]
        public string HRBusinessPartner { get; set; }
        [DataMember]
        public string HRBusinessPartnerCode { get; set; }
        [DataMember]
        public string Appraiser { get; set; }
        [DataMember]
        public string AppraiserCode { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        [DataMember]
        public string ReviewerCode { get; set; }
        [DataMember]
        public string Phase { get; set; }
        [DataMember]
        public string Rating { get; set; }
        [DataMember]
        public string PerformanceIssue { get; set; }
        [DataMember]
        public string MidTermEvalComments { get; set; }
        [DataMember]
        public string MidFinalEvalComments { get; set; }
        [DataMember]
        public string OldEmployeeCode { get; set; }
        [DataMember]
        public string Dummy { get; set; }
    }

    public class PerformanceRatingEntity
    {
        [DataMember]
        public int numberScale { get; set; }
        [DataMember]
        public string PercentageAchievement { get; set; }
    }

    public class PerformnceCycleEntity
    {
        [DataMember]
        public string PerformanceCycle { get; set; }
    }

    [MessageContract]
    public class MyReportMessageContractRequest
    {
        [MessageBodyMember(Order = 0)]
        public int OrderID { get; set; }
        [MessageBodyMember(Order = 1)]
        public string ItemStatus { get; set; }
    }

    [MessageContract]
    public class MyReportMessageContractResponse
    {
        [MessageBodyMember]
        public List<ParametersEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class CompanyRequest
    {
        [MessageBodyMember(Order = 0)]
        public string RegionIds { get; set; }
    }

    [MessageContract]
    public class CompanyResponse
    {
        [MessageBodyMember]
        public List<CompanyEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class EligibleEmployeesRequest
    {
        [MessageBodyMember(Order = 0)]
        public string RegionIds { get; set; }
        [MessageBodyMember(Order = 1)]
        public string CompanyIds { get; set; }
        [MessageBodyMember(Order = 2)]
        public string OrganizationUnitIds { get; set; }
    }

    [MessageContract]
    public class EligibleEmployeesResponse
    {
        [MessageBodyMember]
        public List<ParametersEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class EmployeeWiseRequest
    {
        [MessageBodyMember(Order = 0)]
        public int PerformanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 3)]
        public string Organization { get; set; }
    }

    [MessageContract]
    public class EmployeeWiseResponse
    {
        [MessageBodyMember]
        public List<ParametersEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class CompletionStatusRequest
    {
        [MessageBodyMember(Order = 0)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Phase { get; set; }
    }

    [MessageContract]
    public class CompletionStatusResponse
    {
        [MessageBodyMember]
        public List<CompletionStatusEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class RatingTrendRequest
    {
        [MessageBodyMember(Order = 0)]
        public string PerformanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Phase { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 3)]
        public string Country { get; set; }
        ////[MessageBodyMember(Order = 4)]
        ////public string OrganizationUnit { get; set; }

    }

    [MessageContract]
    public class RatingTrendResponse
    {
        [MessageBodyMember]
        public List<RatingTrendEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class PDPRequest
    {
        [MessageBodyMember(Order = 0)]
        public string PerformanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 3)]
        public string OrganizationUnit { get; set; }
    }

    [MessageContract]
    public class PDPResponse
    {
        [MessageBodyMember]
        public List<PDPEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class HRReviewRequest
    {
        [MessageBodyMember(Order = 0)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Phase { get; set; }
    }

    [MessageContract]
    public class HRReviewResponse
    {
        [MessageBodyMember]
        public List<PDPEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class PIPRequest
    {
        [MessageBodyMember(Order = 0)]
        public string PerformanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 2)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 3)]
        public string OrganizationUnit { get; set; }
    }

    [MessageContract]
    public class PIPResponse
    {
        [MessageBodyMember]
        public List<PIPEntity> DataItems { get; set; }
    }
    [MessageContract]
    public class RegionRequest
    {

    }
    [MessageContract]
    public class RegionResponse
    {
        [MessageBodyMember]
        public List<RegionEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class StatusRequest
    {
        [MessageBodyMember(Order = 0)]
        public string Region { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Country { get; set; }
        [MessageBodyMember(Order = 2)]
        public string OrganizationUnit { get; set; }
    }

    [MessageContract]
    public class StatusResponse
    {
        [MessageBodyMember]
        public List<ParametersEntity> DataItems { get; set; }
    }

    [MessageContract]
    public class AllRatingTrendRequest
    {
        [MessageBodyMember(Order = 0)]
        public string PerformanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string Phase { get; set; }
    }
    #region Appraisal total Details Report

    [MessageContract]
    public class AppraisalAllDataRequest
    {
        [MessageBodyMember(Order = 0)]
        public string performanceCycle { get; set; }
        [MessageBodyMember(Order = 1)]
        public string employeeCode { get; set; }
    }

    [MessageContract]
    public class GoalsResponse
    {
        [MessageBodyMember]
        public List<GoalsEntity> DataItems { get; set; }
    }

    [DataContract]
    public class GoalsEntity
    {
        [DataMember]
        public string appraiseeComments { get; set; }
        [DataMember]
        public string appraiserComments { get; set; }
        [DataMember]
        public string dueDate { get; set; }
        [DataMember]
        public string evalution { get; set; }
        [DataMember]
        public string goal { get; set; }
        [DataMember]
        public string goalCategory { get; set; }
        [DataMember]
        public string goalDescription { get; set; }
        [DataMember]
        public string reviewerComments { get; set; }
        [DataMember]
        public string score { get; set; }
        [DataMember]
        public string weightage { get; set; }
    }

    [MessageContract]
    public class CompetenciesResponse
    {
        [MessageBodyMember]
        public List<CompetenciesEntity> DataItems { get; set; }
    }

    [DataContract]
    public class CompetenciesEntity
    {
        [DataMember]
        public string appraiseeComments { get; set; }
        [DataMember]
        public string appraiserComments { get; set; }
        [DataMember]
        public string competency { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string expectedResult { get; set; }
        [DataMember]
        public string rating { get; set; }
        [DataMember]
        public string reviewerComments { get; set; }
    }

    [MessageContract]
    public class DevelopmentMeasuresResponse
    {
        [MessageBodyMember]
        public List<DevelopmentMeasuresEntity> DataItems { get; set; }
    }

    [DataContract]
    public class DevelopmentMeasuresEntity
    {
        [DataMember]
        public string H1AppraiseeComments { get; set; }
        [DataMember]
        public string H1AppraiserComments { get; set; }
        [DataMember]
        public string H2AppraiseeComments { get; set; }
        [DataMember]
        public string H2AppraiserComments { get; set; }
        [DataMember]
        public string nextSteps { get; set; }
        [DataMember]
        public string when { get; set; }
        [DataMember]
        public string what { get; set; }
    }

    [MessageContract]
    public class AppraisalPIPResponse
    {
        [MessageBodyMember]
        public List<AppraisalPIPEntity> DataItems { get; set; }
    }

    [DataContract]
    public class AppraisalPIPEntity
    {
        [DataMember]
        public string performanceIssue { get; set; }
        [DataMember]
        public string expectedAchievement { get; set; }
        [DataMember]
        public string timeFrame { get; set; }
        [DataMember]
        public string midTermActualResult { get; set; }
        [DataMember]
        public string midTermAppraisersAssessment { get; set; }
        [DataMember]
        public string finalActualResult { get; set; }
        [DataMember]
        public string finalTermAppraisersAssessment { get; set; }
    }

    ////[MessageContract]
    ////public class AppraisalDataRequest
    ////{
    ////    [MessageBodyMember(Order = 0)]
    ////    public string appraiserCode { get; set; }

    ////    [MessageBodyMember(Order = 1)]
    ////    public string performanceCycle { get; set; }
    ////}

    [MessageContract]
    public class AppraisalDataResponse
    {
        [MessageBodyMember]
        public List<AppraisalEntity> DataItems { get; set; }
    }

    [DataContract]
    public class AppraisalEntity
    {
        [DataMember]
        public string appriaseName { get; set; }
        [DataMember]
        public string appriaserName { get; set; }
        [DataMember]
        public string reviewerName { get; set; }
        [DataMember]
        public string hrBusinessPartnerName { get; set; }
        [DataMember]
        public string region { get; set; }
        [DataMember]
        public string company { get; set; }
        [DataMember]
        public string organization { get; set; }
        [DataMember]
        public string dueDate { get; set; }
        [DataMember]
        public string confirmationDuedate { get; set; }
        [DataMember]
        public string appriaseCode { get; set; }
        [DataMember]
        public string appriaserCode { get; set; }
        [DataMember]
        public string reviewerCode { get; set; }
        [DataMember]
        public string hrBusinessPartnerCode { get; set; }
        [DataMember]
        public string position { get; set; }
        [DataMember]
        public string hireDate { get; set; }

    }

    #endregion
}
