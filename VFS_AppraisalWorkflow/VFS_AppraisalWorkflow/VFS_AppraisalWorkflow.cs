using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;

namespace VFS_AppraisalWorkflow.VFS_AppraisalWorkflow
{
    public sealed partial class VFS_AppraisalWorkflow : StateMachineWorkflowActivity
    {
        public VFS_AppraisalWorkflow()
        {
            InitializeComponent();
        }

        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();

        public Guid createTaskForAppraiserGoalVerification_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForAppraiserGoalVerification_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForAppraiserGoalVerification_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForAppraiserGoalVerification_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForSelfEvaluation_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForSelfEvaluation_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedSelfEvaluation_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedSelfEvaluation_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForAppraiserEvaluation_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForAppraiserEvaluation_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForAppraiserEvaluation_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForAppraiserEvaluation_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForReviewerVerification_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForReviewerVerification_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedReviewerVerification_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedReviewerVerification_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForAppraiseeSignOff_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForAppraiseeSignOff_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForAppraiseeSignOff_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForAppraiseeSignOff_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForCountryHR_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForCountryHR_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForCountryHR_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForCountryHR_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForInitialGoalSetting_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForInitialGoalSetting_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedGoalSetting_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedGoalSetting_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH1Empty_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH1Empty_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onH1EmptyTaskChanged_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onH1EmptyTaskChanged_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public String logToHistory1_HistoryDescription = default(System.String);
        public String logToHistory1_HistoryOutcome = default(System.String);

        bool selfEvaluation = false;
        bool isAppraiseeSignOff = false;
        bool isHRRequest = false;
        bool h2SelfEvaluation = false;
        bool h2Statrted = false;
        bool deActivation = false;
        string assignedUser = string.Empty;

        private void CheckH1orH2(object sender, EventArgs e)
        {
            if (Convert.ToString(workflowProperties.Item["appAppraisalStatus"]) == "H2 - Awaiting Appraisee Goal Setting")
            {
                h2Statrted = true;
            }
        }

        private void CheckH2Started(object sender, ConditionalEventArgs e)
        {
            if (h2Statrted)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForInitialGoalSetting_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForInitialGoalSetting);
                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());
                SPUser appraisee = workflowProperties.Web.EnsureUser(assignedUser);

                string wfStatus = GetStatus(1);
                createTaskForInitialGoalSetting_TaskId = Guid.NewGuid();
                createTaskForInitialGoalSetting_TaskProperties = new SPWorkflowTaskProperties();

                CreateTasksCommon(createTaskForInitialGoalSetting_TaskProperties, workflowProperties, wfStatus, appraisee);

                //    createTaskForInitialGoalSetting_TaskProperties.Title = "Please Approve The Task";               
                //    createTaskForInitialGoalSetting_TaskProperties.AssignedTo = appraisee.LoginName;
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //    createTaskForInitialGoalSetting_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);
                if (CanSendMail())
                    SendMail(appraisee, "H1-Awaiting Appraisee Goal Setting", "H1-Awaiting Appraisee Goal Setting");
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedGoalSetting_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedGoalSetting_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedGoalSetting_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H1AppraiseeDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForAppraiserGoalVerification_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForAppraiserGoalVerification);

                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Appraiser");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appAppraiserCode"].ToString());

                string wfStatus = GetStatus(2);

                createTaskForAppraiserGoalVerification_TaskId = Guid.NewGuid();
                createTaskForAppraiserGoalVerification_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForAppraiserGoalVerification_TaskProperties, workflowProperties, wfStatus, appraiser);

                //createTaskForAppraiserGoalVerification_TaskProperties.Title = "Please Approve The Task";
                //createTaskForAppraiserGoalVerification_TaskProperties.AssignedTo = appraiser.LoginName;
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForAppraiserGoalVerification_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);

                logToHistory1_HistoryDescription = "";
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }

        }

        private void onTaskChangedForAppraiserGoalVerification_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                deActivation = false;
                Hashtable ht = this.onTaskChangedForAppraiserGoalVerification_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForAppraiserGoalVerification_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                        else
                        {
                            SendMail(null, "H1-Goals Approved", "H1-Goals Approved");
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H1AppraiserDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void caForTMTInitiation_ExecuteCode(object sender, EventArgs e)
        {
            try
            {
                selfEvaluation = false;
                SPList listTMT = workflowProperties.Web.Lists["TMT Actions"];
                SPQuery q = new SPQuery();
                q.Query = "<Where><And><Eq><FieldRef Name='tmtPerformanceCycle' /><Value Type='Text'>" + Convert.ToString(workflowProperties.Item["appPerformanceCycle"]) + "</Value></Eq><Eq><FieldRef Name='tmtIsH1SelfEvaluationStarted' /><Value Type='Text'>Started</Value></Eq></And></Where>";
                SPListItemCollection coll = listTMT.GetItems(q);
                if (coll.Count > 0)
                {
                    foreach (SPListItem item in coll)
                    {
                        if (Convert.ToString(item["tmtIsH1SelfEvaluationStarted"]) == "Started")
                            selfEvaluation = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void TMTInitiates(object sender, ConditionalEventArgs e)
        {
            if (selfEvaluation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH1Empty_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH1Empty);
                string wfStatus = GetStatus(3);

                createTaskForH1Empty_TaskId = Guid.NewGuid();
                createTaskForH1Empty_TaskProperties = new SPWorkflowTaskProperties();


                CreateTasksCommon(createTaskForH1Empty_TaskProperties, workflowProperties, wfStatus, null);

            }
            catch (Exception)
            {
            }
        }

        private void onH1EmptyTaskChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onH1EmptyTaskChanged_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower() || key.ToString() == "tskStatus")
                    {
                        if (this.onH1EmptyTaskChanged_AfterProperties.ExtendedProperties[key] != null)
                        {
                            if (this.onH1EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "H1-Amend Goals")
                            {
                                selfEvaluation = false;
                                SendMail(null, "H1-Amend Goals", "H1-Amend Goals");
                            }
                            else if (this.onH1EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "H1-Awaiting Self-evaluation")
                            {
                                selfEvaluation = true;
                            }
                            else if (this.onH1EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                            {
                                deActivation = true;
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {

            }
        }

        private void H1EmptyDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForSelfEvaluation_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForSelfEvaluation);

                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());
                string wfStatus = GetStatus(4);
                createTaskForSelfEvaluation_TaskId = Guid.NewGuid();
                createTaskForSelfEvaluation_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraisee = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForSelfEvaluation_TaskProperties, workflowProperties, wfStatus, appraisee);

                //createTaskForSelfEvaluation_TaskProperties.Title = "Please Approve The Task";            
                //createTaskForSelfEvaluation_TaskProperties.AssignedTo = appraisee.LoginName;
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForSelfEvaluation_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);
                SendMail(appraisee, "H1-Awaiting Self-evaluation", "H1-Awaiting Self-evaluation");
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedSelfEvaluation_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedSelfEvaluation_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedSelfEvaluation_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                        else
                        {
                            SendMail(null, "H1-Awaiting Appraiser Evaluation", "H1-Awaiting Appraiser Evaluation");
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H1SelfDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForAppraiserEvaluation_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForAppraiserEvaluation);
                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Appraiser");

                assignedUser = GetAssignedToUser(appraiserCode);  // workflowProperties.Item["appAppraiserCode"].ToString());
                string wfStatus = GetStatus(5);
                createTaskForAppraiserEvaluation_TaskId = Guid.NewGuid();
                createTaskForAppraiserEvaluation_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);
                createTaskForAppraiserEvaluation_TaskProperties.Title = "Please Approve The Task";

                CreateTasksCommon(createTaskForAppraiserEvaluation_TaskProperties, workflowProperties, wfStatus, appraiser);

                //createTaskForAppraiserEvaluation_TaskProperties.AssignedTo = appraiser.LoginName;
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForAppraiserEvaluation_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);

                //SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedForAppraiserEvaluation_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedForAppraiserEvaluation_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForAppraiserEvaluation_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H1AppraiserEvalDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForReviewerVerification_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForReviewerVerification);

                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Reviewer");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appReviewerCode"].ToString());
                string wfStatus = GetStatus(6);
                SPUser reviewer = workflowProperties.Web.EnsureUser(assignedUser);

                createTaskForReviewerVerification_TaskId = Guid.NewGuid();
                createTaskForReviewerVerification_TaskProperties = new SPWorkflowTaskProperties();

                CreateTasksCommon(createTaskForReviewerVerification_TaskProperties, workflowProperties, wfStatus, reviewer);

                //createTaskForReviewerVerification_TaskProperties.Title = "Please Approve The Task";
                //createTaskForReviewerVerification_TaskProperties.AssignedTo = reviewer.LoginName;
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForReviewerVerification_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);
                SendMail(reviewer, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedReviewerVerification_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedReviewerVerification_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedReviewerVerification_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H1RvrEvalDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForAppraiseeSignOff_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForAppraiseeSignOff);

                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());
                string wfStatus = GetStatus(7);

                createTaskForAppraiseeSignOff_TaskId = Guid.NewGuid();
                createTaskForAppraiseeSignOff_TaskProperties = new SPWorkflowTaskProperties();

                SPUser appraisee = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForAppraiseeSignOff_TaskProperties, workflowProperties, wfStatus, appraisee);

                //createTaskForAppraiseeSignOff_TaskProperties.Title = "Please Approve The Task";                
                //createTaskForAppraiseeSignOff_TaskProperties.AssignedTo = appraisee.LoginName;
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForAppraiseeSignOff_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);
                SendMail(appraisee, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedForAppraiseeSignOff_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedForAppraiseeSignOff_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForAppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Sign Off")
                        {
                            isAppraiseeSignOff = true;
                        }
                        else if (this.onTaskChangedForAppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Appeal")
                        {
                            isAppraiseeSignOff = false;
                            SendMail(null, "H1 - Appraisee Appeal", "H1 - Appraisee Appeal");
                        }
                        else if (this.onTaskChangedForAppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                    if (key.ToString() == "276476E9-E94E-418D-B752-200A71F722D6".ToLower())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.onTaskChangedForAppraiseeSignOff_AfterProperties.ExtendedProperties[key])))
                        {
                            SendMail(null, "H1-HR Sign Off", "H1-HR Sign Off");
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void H1AppraiseeSignOffDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void AppraiseeAppeal(object sender, ConditionalEventArgs e)
        {
            if (!isAppraiseeSignOff)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForCountryHR_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForCountryHR);

                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "HR");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appHRBusinessPartnerCode"].ToString());
                string wfStatus = GetStatus(8);

                createTaskForCountryHR_TaskId = Guid.NewGuid();
                createTaskForCountryHR_TaskProperties = new SPWorkflowTaskProperties();

                SPUser hr = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForCountryHR_TaskProperties, workflowProperties, wfStatus, hr);

                //createTaskForCountryHR_TaskProperties.Title = "Please Approve The Task";                
                //createTaskForCountryHR_TaskProperties.AssignedTo = hr.LoginName;
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["Status"] = "Not started";
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //createTaskForCountryHR_TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);
                SendMail(hr, wfStatus, wfStatus);
            }
            catch (Exception)
            {


            }
        }

        private void onTaskChangedForCountryHR_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedForCountryHR_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForCountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Request")
                        {
                            isHRRequest = true;
                            SendMail(null, "H1 - HR Review", "H1 - HR Review");
                        }
                        else if (this.onTaskChangedForCountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Close")
                        {
                            isHRRequest = false;
                        }
                        else if (this.onTaskChangedForCountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void H1HrDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void CountryHRClose(object sender, ConditionalEventArgs e)
        {
            if (!isHRRequest)
                e.Result = true;
            else
                e.Result = false;
        }

        private void SetContentType(CreateTaskWithContentType createTask)
        {
            try
            {
                if (this.workflowProperties.TaskList.ContentTypesEnabled != true)
                {
                    workflowProperties.TaskList.ContentTypesEnabled = true;
                }

                SPContentTypeId myContentTypeId = new SPContentTypeId(createTask.ContentTypeId);
                SPContentType myContentType = workflowProperties.Site.RootWeb.ContentTypes[myContentTypeId];
                bool contentTypeExists = false;
                string cname = myContentType.Name;
                foreach (SPContentType contentType in workflowProperties.TaskList.ContentTypes)
                {
                    if (contentType.Name == myContentType.Name)
                    {
                        contentTypeExists = true;
                        break;
                    }
                }
                if (contentTypeExists != true)
                {
                    workflowProperties.TaskList.ContentTypes.Add(myContentType);
                }
            }
            catch (Exception)
            {

            }
        }

        private void CreateTasksCommon(SPWorkflowTaskProperties TaskProperties, SPWorkflowActivationProperties workflowProperties, string wfStatus, SPUser assignedTo)
        {
            try
            {
                TaskProperties.Title = "Please Approve The Task";
                if (assignedTo != null)
                    TaskProperties.AssignedTo = assignedTo.LoginName;
                TaskProperties.ExtendedProperties["Status"] = "Not started";
                TaskProperties.ExtendedProperties["tskStatus"] = wfStatus;
                TaskProperties.ExtendedProperties["tskAppraisalId"] = Convert.ToInt32(workflowProperties.ItemId);
                TaskProperties.ExtendedProperties["tskAppraiseeCode"] = Convert.ToString(workflowProperties.Item["appEmployeeCode"]);
                //TaskProperties.ExtendedProperties["tskAppraiserCode"] = Convert.ToString(workflowProperties.Item["appAppraiserCode"]);
                //TaskProperties.ExtendedProperties["tskReviewerCode"] = Convert.ToString(workflowProperties.Item["appReviewerCode"]);
                //TaskProperties.ExtendedProperties["tskHRCode"] = Convert.ToString(workflowProperties.Item["appHRBusinessPartnerCode"]);

            }
            catch (Exception)
            {
            }
        }

        private string GetStatus(int Id)
        {
            try
            {
                SPList lstAppraisalStatus = null;
                SPListItem lstStatusItem = null;

                lstAppraisalStatus = workflowProperties.Web.Lists["Appraisal Status"];
                lstStatusItem = lstAppraisalStatus.GetItemById(Id);
                return Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetAssignedToUser(string employeeCode)
        {
            try
            {
                SPList lstEmployeeMaster = workflowProperties.Web.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                SPListItem employeeItem = masterCollection[0];

                return Convert.ToString(employeeItem["WindowsLogin"]);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetCodesByAppraisee(string employeeCode, string flag)
        {
            try
            {
                SPList lstEmployeeMaster = workflowProperties.Web.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                SPListItem employeeItem = masterCollection[0];
                if (flag == "Appraiser")
                    return Convert.ToString(employeeItem["ReportingManagerEmployeeCode"]);
                else if (flag == "Reviewer")
                    return Convert.ToString(employeeItem["DepartmentHeadEmployeeCode"]);
                else if (flag == "HR")
                    return Convert.ToString(employeeItem["HRBusinessPartnerEmployeeCode"]);
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// H2 Started
        /// </summary>

        public Guid createTaskForH2InitialGoalSetting_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2InitialGoalSetting_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedH2GoalSetting_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedH2GoalSetting_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForAppraiserH2GoalVerification_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForAppraiserH2GoalVerification_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForAppraiserH2GoalVerification_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForAppraiserH2GoalVerification_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2SelfEvaluation_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2SelfEvaluation_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedH2SelfEvaluation_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedH2SelfEvaluation_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2AppraiserEvaluation_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2AppraiserEvaluation_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForH2AppraiserEvaluation_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForH2AppraiserEvaluation_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2ReviewerVerification_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2ReviewerVerification_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedH2ReviewerVerification_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedH2ReviewerVerification_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2AppraiseeSignOff_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2AppraiseeSignOff_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForH2AppraiseeSignOff_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForH2AppraiseeSignOff_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2CountryHR_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2CountryHR_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onTaskChangedForH2CountryHR_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedForH2CountryHR_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public Guid createTaskForH2Empty_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForH2Empty_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        public SPWorkflowTaskProperties onH2EmptyTaskChanged_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onH2EmptyTaskChanged_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void createTaskForH2InitialGoalSetting_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2InitialGoalSetting);


                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());
                SPUser appraisee = workflowProperties.Web.EnsureUser(assignedUser);

                string wfStatus = GetStatus(12);
                createTaskForH2InitialGoalSetting_TaskId = Guid.NewGuid();
                createTaskForH2InitialGoalSetting_TaskProperties = new SPWorkflowTaskProperties();

                CreateTasksCommon(createTaskForH2InitialGoalSetting_TaskProperties, workflowProperties, wfStatus, appraisee);
                SendMail(appraisee, "H1 – Completed", "H1 – Completed");
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedH2GoalSetting_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedH2GoalSetting_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedH2GoalSetting_AfterProperties.ExtendedProperties[key].ToString() != "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {

            }
        }

        private void IfH1GoalsCompleted(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForAppraiserH2GoalVerification_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForAppraiserH2GoalVerification);
                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Appraiser");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appAppraiserCode"].ToString());

                string wfStatus = GetStatus(13);

                createTaskForAppraiserH2GoalVerification_TaskId = Guid.NewGuid();
                createTaskForAppraiserH2GoalVerification_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForAppraiserH2GoalVerification_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedForAppraiserH2GoalVerification_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                deActivation = false;
                Hashtable ht = this.onTaskChangedForAppraiserH2GoalVerification_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForAppraiserH2GoalVerification_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                        else
                        {
                            SendMail(null, "H2 - Goals Approved", "H2 - Goals Approved");
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H2AppraiserDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2SelfEvaluation_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2SelfEvaluation);

                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());

                string wfStatus = GetStatus(15);

                createTaskForH2SelfEvaluation_TaskId = Guid.NewGuid();
                createTaskForH2SelfEvaluation_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForH2SelfEvaluation_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {
            }
        }

        private void onTaskChangedH2SelfEvaluation_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                deActivation = false;
                Hashtable ht = this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                        else
                        {
                            SendMail(null, "H2 - Awaiting Appraiser Evaluation", "H2 - Awaiting Appraiser Evaluation");
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H2SelfDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2AppraiserEvaluation_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2AppraiserEvaluation);
                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Appraiser");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appAppraiserCode"].ToString());

                string wfStatus = GetStatus(16);

                createTaskForH2AppraiserEvaluation_TaskId = Guid.NewGuid();
                createTaskForH2AppraiserEvaluation_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForH2AppraiserEvaluation_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {
            }
        }

        private void onTaskChangedForH2AppraiserEvaluation_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                deActivation = false;
                Hashtable ht = this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H2AppraiserEvalDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2ReviewerVerification_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2ReviewerVerification);
                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Reviewer");

                assignedUser = GetAssignedToUser(appraiserCode);  //workflowProperties.Item["appReviewerCode"].ToString());

                string wfStatus = GetStatus(17);

                createTaskForH2ReviewerVerification_TaskId = Guid.NewGuid();
                createTaskForH2ReviewerVerification_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForH2ReviewerVerification_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {
            }
        }

        private void onTaskChangedH2ReviewerVerification_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                deActivation = false;
                Hashtable ht = this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedH2SelfEvaluation_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        private void H2ReviewerDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2AppraiseeSignOff_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2AppraiseeSignOff);

                assignedUser = GetAssignedToUser(workflowProperties.Item["appEmployeeCode"].ToString());

                string wfStatus = GetStatus(18);

                createTaskForH2AppraiseeSignOff_TaskId = Guid.NewGuid();
                createTaskForH2AppraiseeSignOff_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForH2AppraiseeSignOff_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {

            }
        }

        private void onTaskChangedForH2AppraiseeSignOff_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedForH2AppraiseeSignOff_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;

                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForH2AppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Sign Off")
                        {
                            isAppraiseeSignOff = true;

                        }
                        else if (this.onTaskChangedForH2AppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Appeal")
                        {
                            isAppraiseeSignOff = false;
                            SendMail(null, "H2 - Appraisee Appeal", "H2 - Appraisee Appeal");
                        }
                        else if (this.onTaskChangedForH2AppraiseeSignOff_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                    if (key.ToString() == "276476E9-E94E-418D-B752-200A71F722D6".ToLower())
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.onTaskChangedForH2AppraiseeSignOff_AfterProperties.ExtendedProperties[key])))
                        {
                            SendMail(null, "H2-HR Sign Off", "H2-HR Sign Off");
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void H2SignOffDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void H2AppraiseeAppeal(object sender, ConditionalEventArgs e)
        {
            if (!isAppraiseeSignOff)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2CountryHR_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2CountryHR);
                string appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "HR");

                assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appHRBusinessPartnerCode"].ToString());

                string wfStatus = GetStatus(19);

                createTaskForH2CountryHR_TaskId = Guid.NewGuid();
                createTaskForH2CountryHR_TaskProperties = new SPWorkflowTaskProperties();
                SPUser appraiser = workflowProperties.Web.EnsureUser(assignedUser);

                CreateTasksCommon(createTaskForH2CountryHR_TaskProperties, workflowProperties, wfStatus, appraiser);
                SendMail(appraiser, wfStatus, wfStatus);
            }
            catch (Exception)
            {
            }
        }

        private void onTaskChangedForH2CountryHR_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onTaskChangedForH2CountryHR_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower())
                    {
                        if (this.onTaskChangedForH2CountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Request")
                        {
                            isHRRequest = true;
                            SendMail(null, "H2 - HR Review", "H2 - HR Review");
                        }
                        else if (this.onTaskChangedForH2CountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Close")
                        {
                            isHRRequest = false;
                            SendMail(null, "H2 – Completed", "H2 – Completed");
                        }
                        else if (this.onTaskChangedForH2CountryHR_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                        {
                            deActivation = true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void H2HRDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void caForTMTInitiation_ExecuteCodecaForH2TMTInitiation_ExecuteCode(object sender, EventArgs e)
        {
            try
            {
                h2SelfEvaluation = false;
                SPList listTMT = workflowProperties.Web.Lists["TMT Actions"];
                SPQuery q = new SPQuery();
                q.Query = "<Where><And><Eq><FieldRef Name='tmtPerformanceCycle' /><Value Type='Text'>" + Convert.ToString(workflowProperties.Item["appPerformanceCycle"]) + "</Value></Eq><Eq><FieldRef Name='tmtIsH2SelfEvaluationStarted' /><Value Type='Text'>Started</Value></Eq></And></Where>";
                SPListItemCollection coll = listTMT.GetItems(q);
                if (coll.Count > 0)
                {
                    foreach (SPListItem item in coll)
                    {
                        if (Convert.ToString(item["tmtIsH2SelfEvaluationStarted"]) == "Started")
                            h2SelfEvaluation = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void H2TMTInitiates(object sender, ConditionalEventArgs e)
        {
            if (h2SelfEvaluation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void H2CountryHRClose(object sender, ConditionalEventArgs e)
        {
            if (!isHRRequest)
                e.Result = true;
            else
                e.Result = false;
        }

        //private void H2SelfEvaluationByTMT(object sender, ConditionalEventArgs e)
        //{
        //    if (h2SelfEvaluation)
        //        e.Result = true;
        //    else
        //        e.Result = false;
        //}

        private void H1SelfEvaluationStarted(object sender, ConditionalEventArgs e)
        {
            if (selfEvaluation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void createTaskForH2Empty_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForH2Empty);
                string wfStatus = GetStatus(14);

                createTaskForH2Empty_TaskId = Guid.NewGuid();
                createTaskForH2Empty_TaskProperties = new SPWorkflowTaskProperties();

                CreateTasksCommon(createTaskForH2Empty_TaskProperties, workflowProperties, wfStatus, null);

            }
            catch (Exception)
            {
            }
        }

        private void onH2EmptyTaskChanged_Invoked(object sender, ExternalDataEventArgs e)
        {
            try
            {
                Hashtable ht = this.onH2EmptyTaskChanged_AfterProperties.ExtendedProperties;
                ICollection keys = ht.Keys;

                h2SelfEvaluation = false;
                deActivation = false;
                foreach (object key in keys)
                {
                    if (key.ToString() == "5E72C32E-ABA2-405C-81DC-93F4D870AD61".ToLower() || key.ToString() == "tskStatus")
                    {
                        if (this.onH2EmptyTaskChanged_AfterProperties.ExtendedProperties[key] != null)
                        {
                            if (this.onH2EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "H2-Amend Goals")
                            {
                                h2SelfEvaluation = false;
                                SendMail(null, "H2-Amend Goals", "H2-Amend Goals");
                            }
                            else if (this.onH2EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "H2 - Awaiting Self-evaluation")
                            {
                                h2SelfEvaluation = true;
                            }
                            else if (this.onH2EmptyTaskChanged_AfterProperties.ExtendedProperties[key].ToString() == "Deactivation")
                            {
                                deActivation = true;
                            }
                        }
                    }
                }
            }

            catch (Exception)
            {

            }
        }

        private void H2EmptyDeactivation(object sender, ConditionalEventArgs e)
        {
            if (deActivation)
                e.Result = true;
            else
                e.Result = false;
        }

        private void H2SelfEvaluationStarted(object sender, ConditionalEventArgs e)
        {
            if (h2SelfEvaluation)
                e.Result = true;
            else
                e.Result = false;
        }

        public Guid createTaskForDeactivation_TaskId = default(System.Guid);
        public SPWorkflowTaskProperties createTaskForDeactivation_TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedH1Deactivation_AfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
        public SPWorkflowTaskProperties onTaskChangedH1Deactivation_BeforeProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

        private void createTaskForDeactivation_MethodInvoking(object sender, EventArgs e)
        {
            try
            {
                SetContentType(createTaskForDeactivation);
                string wfStatus = "Awiting H2 Activation";  // GetStatus(14);

                createTaskForDeactivation_TaskId = Guid.NewGuid();
                createTaskForDeactivation_TaskProperties = new SPWorkflowTaskProperties();

                CreateTasksCommon(createTaskForDeactivation_TaskProperties, workflowProperties, wfStatus, null);

            }
            catch (Exception)
            {
            }
        }

        private void onTaskChangedH1Deactivation_Invoked(object sender, ExternalDataEventArgs e)
        {

        }

        private void SendMail(SPUser toUser, string status, string body)
        {
            try
            {
                string toUsers = string.Empty;
                string mailBody = string.Empty;
                string subject = string.Empty;
                string mail = string.Empty;
                StringDictionary headers;
                string DBurl = workflowProperties.WebUrl + DashBoardUrl;
                headers = new StringDictionary();
                switch (status)
                {
                    case "H1-Awaiting Appraisee Goal Setting":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingAppraiseeGoalSetting, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Awaiting Appraiser Goal Approval":
                        {

                            mail = string.Format(AppraisalMailFormats.H1AwaitingAppraiserGoalApproval, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);

                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Goals Approved":
                        {
                            mail = string.Format(AppraisalMailFormats.H1GoalsApproved, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraiser").Name, DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Amend Goals":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingAppraiserGoalApproval21, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Awaiting Self-evaluation":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingSelfevaluation1, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Awaiting Appraiser Evaluation":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingAppraiserEvaluation, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1-Awaiting Reviewer Approval":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingReviewerApproval, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Reviewer").Name, GetMailUserName("Appraiser").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), DBurl);
                            headers.Add("to", GetMailUserName("Reviewer").Email);
                            headers.Add("cc", GetMailUserName("Appraiser").Email + "," + GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1 - Awaiting Appraisee Sign-off":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AwaitingAppraiseeSignoff, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, GetMailUserName("Reviewer").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1 - Sign-off by Appraisee":
                        {
                            mail = string.Format(AppraisalMailFormats.H1SignoffbyAppraisee, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("HR").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("HR").Email);
                            break;
                        }
                    case "H1-HR Sign Off":
                        {
                            mail = string.Format(AppraisalMailFormats.H1HRSignOff,
                                Convert.ToString(workflowProperties.Item["appPerformanceCycle"]),
                                GetMailUserName("Appraisee").Name,
                                GetMailUserName("Appraisee").Name,
                                Convert.ToString(workflowProperties.Item["appPerformanceCycle"]),
                                GetMailUserName("HR").Name,
                                DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            headers.Add("cc", GetMailUserName("HR").Email + "," + GetMailUserName("Appraiser").Email);
                            break;
                        }
                    case "H1 – Completed":
                        {
                            mail = string.Format(AppraisalMailFormats.H1Completed, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1 - HR Review":
                        {
                            mail = string.Format(AppraisalMailFormats.H1HRReview, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("HR").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Reviewer").Email + "," + GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H1 - Appraisee Appeal":
                        {
                            mail = string.Format(AppraisalMailFormats.H1AppraiseeAppeal, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Reviewer").Email + "," + GetMailUserName("Appraisee").Email);
                            break;
                        }

                    //case "H2 - Awaiting Appraisee Goal Setting":
                    //    {
                    //        mail = string.Format(AppraisalMailFormats.H2AwaitingAppraiseeGoalSetting, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), toUser.Name, toUser.Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DashBoardUrl);

                    //        break;
                    //    }
                    case "H2 - Awaiting Appraiser Goal Approval":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingAppraiserGoalApproval, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Goals Approved":
                        {
                            mail = string.Format(AppraisalMailFormats.H2GoalsApproved, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }

                    case "H2-Amend Goals":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingAppraiserGoalApproval21, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Awaiting Self-evaluation":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingSelfevaluation, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Awaiting Appraiser Evaluation":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingAppraiserEvaluation, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Awaiting Reviewer Approval":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingReviewerApproval, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Reviewer").Name, GetMailUserName("Appraiser").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appEmployeeCode"]), DBurl);
                            headers.Add("to", GetMailUserName("Reviewer").Email);
                            headers.Add("cc", GetMailUserName("Appraiser").Email + "," + GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Awaiting Appraisee Sign-off":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AwaitingAppraiseeSignoff, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, GetMailUserName("Reviewer").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2-HR Sign Off":
                        {
                            mail = string.Format(AppraisalMailFormats.H2HRSignOff, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("HR").Name, DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            headers.Add("cc", GetMailUserName("HR").Email + "," + GetMailUserName("Appraiser").Email);
                            break;
                        }
                    case "H2 - Sign-off by Appraisee":
                        {
                            mail = string.Format(AppraisalMailFormats.H2SignoffbyAppraisee, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("HR").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("HR").Email);
                            break;
                        }
                    case "H2 – Completed":
                        {
                            mail = string.Format(AppraisalMailFormats.H2Completed, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - HR Review":
                        {
                            mail = string.Format(AppraisalMailFormats.H2HRReview, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("HR").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Reviewer").Email + "," + GetMailUserName("Appraisee").Email);
                            break;
                        }
                    case "H2 - Appraisee Appeal":
                        {
                            mail = string.Format(AppraisalMailFormats.H2AppraiseeAppeal, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), GetMailUserName("Appraisee").Name, GetMailUserName("Appraiser").Name, GetMailUserName("Appraisee").Name, Convert.ToString(workflowProperties.Item["appPerformanceCycle"]), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser").Email);
                            headers.Add("cc", GetMailUserName("Appraisee").Email + "," + GetMailUserName("Reviewer").Email);
                            break;
                        }

                }
                subject = mail.Split('%')[0];
                mailBody = mail.Split('%')[1];
                headers.Add("subject", subject);
                headers.Add("content-type", "text/html");
                SPUtility.SendEmail(workflowProperties.Web, headers, mailBody);
            }
            catch (Exception ex)
            {

            }
        }

        private SPUser GetMailUserName(string userCode)
        {
            string appraiserCode = string.Empty;
            SPUser spuser = null;
            switch (userCode)
            {
                case "Appraiser":
                    {
                        appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Appraiser");
                        assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appAppraiserCode"].ToString());
                        spuser = workflowProperties.Web.EnsureUser(assignedUser);
                        break;
                    }
                case "Reviewer":
                    {
                        appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "Reviewer");
                        assignedUser = GetAssignedToUser(appraiserCode);  //workflowProperties.Item["appReviewerCode"].ToString());
                        spuser = workflowProperties.Web.EnsureUser(assignedUser);
                        break;
                    }
                case "HR":
                    {
                        appraiserCode = GetCodesByAppraisee(Convert.ToString(workflowProperties.Item["appEmployeeCode"]), "HR");
                        assignedUser = GetAssignedToUser(appraiserCode); //workflowProperties.Item["appHRBusinessPartnerCode"].ToString());
                        spuser = workflowProperties.Web.EnsureUser(assignedUser);
                        break;
                    }
                case "Appraisee":
                    {
                        string strApprasee = GetAssignedToUser(Convert.ToString(workflowProperties.Item["appEmployeeCode"]));
                        spuser = workflowProperties.Web.EnsureUser(strApprasee);
                        break;
                    }
            }
            return spuser;
        }

        private static string DashBoardUrl
        {
            get
            {
                return "/_layouts/VFS_DashBoards/Dashboard.aspx";
            }
        }

        private bool CanSendMail()
        {
            bool flag = true;
            SPList listPMS = workflowProperties.Web.Lists["PMS Data File Locations"];
            SPQuery q = new SPQuery();
            q.Query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>Send Mails</Value></Eq></Where>";
            SPListItemCollection coll = listPMS.GetItems(q);
            if (coll.Count > 0)
            {
                foreach (SPListItem item in coll)
                {
                    if (Convert.ToString(item["FileLocation"]).ToUpper() == "NO")
                    {
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

    }
}
