using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace VFS_AppraisalWorkflow.VFS_AppraisalWorkflow
{
    public sealed partial class VFS_AppraisalWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind24 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind25 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind26 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition7 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition8 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind27 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind28 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind29 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind30 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind31 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind32 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind33 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind34 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind35 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind36 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind37 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind38 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind39 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind40 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind41 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind42 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind43 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind44 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind45 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind46 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind47 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind48 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind49 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind50 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind51 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind52 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind53 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind54 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind55 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind56 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind57 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind58 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition9 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition10 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition11 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition12 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition13 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition14 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition15 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition16 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition17 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition18 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition19 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition20 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition21 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition22 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition23 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition24 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition25 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind59 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind60 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind61 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind62 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind63 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind64 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind65 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind66 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind67 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind68 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind69 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind70 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind71 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind72 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind73 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind74 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind75 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind76 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken3 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind77 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind78 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind79 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind80 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind81 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind82 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind83 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken4 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind84 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind85 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind86 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken5 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind87 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind88 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind89 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind90 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken6 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind91 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind92 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind93 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind94 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind95 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind96 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind97 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken7 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind98 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind99 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind100 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind101 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind102 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind103 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind104 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken8 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind105 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind106 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind107 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind108 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind109 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind110 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind111 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken9 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind112 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind113 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind114 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind115 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind116 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind117 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind118 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken10 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind119 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind120 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind121 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind122 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind123 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken11 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind124 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind125 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind126 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind127 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind128 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken12 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind129 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind130 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind131 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind132 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind133 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind134 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind135 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken13 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind136 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind137 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind138 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken14 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind139 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind140 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind141 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind142 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken15 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind143 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind144 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind145 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken16 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind146 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind147 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind148 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind149 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken17 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind150 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind151 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind152 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken18 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind153 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind154 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind155 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind156 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken19 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind157 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind158 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind159 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken20 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind160 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind161 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind162 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind163 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken21 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind164 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind165 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind166 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken22 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind167 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind168 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind169 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind170 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken23 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind171 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind172 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind173 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken24 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind174 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind175 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken25 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind176 = new System.Workflow.ComponentModel.ActivityBind();
            this.setStateActivity4 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity40 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity3 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity39 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity2 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity38 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity37 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToH2AppraiserRevaluationHR = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity32 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToClosedState = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity31 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateH2HR = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity29 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToH2AppraiserEvaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity28 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateH2GoalSetting = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity34 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToH2SelfEvaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity20 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToCountryHR = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity11 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToAppraiserRevaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity10 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToAppraiserRevaluationHR = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity14 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToH2InitialGoalSetting = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity13 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToH1GoalChanges = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity2 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToSelfEvaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity1 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifH2GoalsAmendment = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2TMTTrigger = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifGoalsAmendment = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifTMTTrigger = new System.Workflow.Activities.IfElseBranchActivity();
            this.isH2CountryHRRequest = new System.Workflow.Activities.IfElseBranchActivity();
            this.isH2CountryHRClose = new System.Workflow.Activities.IfElseBranchActivity();
            this.isH2AppraiseeSignedOff = new System.Workflow.Activities.IfElseBranchActivity();
            this.isH2AppraiseeAppeal = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseH2TMTInitiationNotStarted = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseH2TMTInitiationStarted = new System.Workflow.Activities.IfElseBranchActivity();
            this.isAppraiseeSignedOff = new System.Workflow.Activities.IfElseBranchActivity();
            this.isAppraiseeAppeal = new System.Workflow.Activities.IfElseBranchActivity();
            this.isCountryHRRequest = new System.Workflow.Activities.IfElseBranchActivity();
            this.isCountryHRClose = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseTMTInitiationNotStarted = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseTMTInitiationStarted = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2Evaluation = new System.Workflow.Activities.IfElseActivity();
            this.terminateActivity4 = new System.Workflow.ComponentModel.TerminateActivity();
            this.ifH1Evaluation = new System.Workflow.Activities.IfElseActivity();
            this.setStateActivity8 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity45 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifElseH2CountryHR = new System.Workflow.Activities.IfElseActivity();
            this.terminateActivity8 = new System.Workflow.ComponentModel.TerminateActivity();
            this.ifElseH2AppraiseeAppeal = new System.Workflow.Activities.IfElseActivity();
            this.terminateActivity7 = new System.Workflow.ComponentModel.TerminateActivity();
            this.setStateToH2AppraiseeSignOffOrAppeal = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity26 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.terminateActivity6 = new System.Workflow.ComponentModel.TerminateActivity();
            this.setStateH2ReviewerVerification = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity24 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.terminateActivity5 = new System.Workflow.ComponentModel.TerminateActivity();
            this.setStateH2AppraiserEvaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity22 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.terminateActivity3 = new System.Workflow.ComponentModel.TerminateActivity();
            this.ifElseH2TMTInitiation = new System.Workflow.Activities.IfElseActivity();
            this.caForH2TMTInitiation = new System.Workflow.Activities.CodeActivity();
            this.terminateActivity2 = new System.Workflow.ComponentModel.TerminateActivity();
            this.terminateActivity1 = new System.Workflow.ComponentModel.TerminateActivity();
            this.logToHistoryListActivity50 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity5 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity18 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setSateToGoalVerification = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity16 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToDeactivation1 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity41 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifElseAppraiseeAppeal = new System.Workflow.Activities.IfElseActivity();
            this.setStateActivity11 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity48 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifElseCountryHR = new System.Workflow.Activities.IfElseActivity();
            this.setStateActivity12 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity49 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToAppraiseeSignOff = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity8 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity10 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity47 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToReviewerVerification = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity6 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity9 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity46 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToAppraiserEvaluation = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity4 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateActivity7 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity44 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.ifElseTMTInitiation = new System.Workflow.Activities.IfElseActivity();
            this.caForTMTInitiation = new System.Workflow.Activities.CodeActivity();
            this.setStateActivity6 = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity43 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.setStateToAppraiseeGoalSetting = new System.Workflow.Activities.SetStateActivity();
            this.setStateToH2GoalSetting = new System.Workflow.Activities.SetStateActivity();
            this.ifElseBranchActivity12 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2EmpttyDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity4 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1EmptyDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity17 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2HRDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity16 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2SignOffDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity15 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2RvrDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity13 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity1 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity11 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2SelfDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity10 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2AppraiserDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity9 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2GoalsCompleted = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1GoalSettingComplete = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1Deactivated = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity7 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1AppraiseeSignOffDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity8 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1HRDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity6 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRvrEvalDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity5 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1AppraiserEvalDeactivated = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity2 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1SelfDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifElseBranchActivity3 = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH1AppraiserDeactivation = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2NotStarted = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifH2Started = new System.Workflow.Activities.IfElseBranchActivity();
            this.setStateToH2Start = new System.Workflow.Activities.SetStateActivity();
            this.logToHistoryListActivity42 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.onTaskChangedH1Deactivation = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity35 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForDeactivation = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2EmptyDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onH2EmptyTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity33 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2Empty = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1Empty = new System.Workflow.Activities.IfElseActivity();
            this.onH1EmptyTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity36 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH1Empty = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2HRDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForH2CountryHR = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity30 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2CountryHR = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2AppraiseeSignOff = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForH2AppraiseeSignOff = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity27 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2AppraiseeSignOff = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2RvrEvalDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedH2ReviewerVerification = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity25 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2ReviewerVerification = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2AppraiserEvalDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForH2AppraiserEvaluation = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity23 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2AppraiserEvaluation = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2SelfDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedH2SelfEvaluation = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity21 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2SelfEvaluation = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2AppraiserDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForAppraiserH2GoalVerification = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity19 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForAppraiserH2GoalVerification = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2GoalSetting = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedH2GoalSetting = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity17 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForH2InitialGoalSetting = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1AppraiseeDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedGoalSetting = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity15 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForInitialGoalSetting = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1AppraiseeSingOffDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForAppraiseeSignOff = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity9 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForAppraiseeSignOff = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1HRDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForCountryHR = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity12 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForCountryHR = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1RvrEvalDeactiovation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedReviewerVerification = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity7 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForReviewerVerification = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifH1AppraiserEvalDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForAppraiserEvaluation = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity5 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForAppraiserEvaluation = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1SelfDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedSelfEvaluation = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistoryListActivity3 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForSelfEvaluation = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH1AppraiserDeactivation = new System.Workflow.Activities.IfElseActivity();
            this.onTaskChangedForAppraiserGoalVerification = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
            this.logToHistory1 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.createTaskForAppraiserGoalVerification = new Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType();
            this.ifElseH2Started = new System.Workflow.Activities.IfElseActivity();
            this.caCheckH1orH2 = new System.Workflow.Activities.CodeActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            this.edH1Deactivation = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH1Deactivation = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2Empty = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2Empty = new System.Workflow.Activities.StateInitializationActivity();
            this.edH1Empty = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH1Empty = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2HR = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2HR = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2AppraiseeSignOff = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2AppraiseeSignOff = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2ReviewerVerification = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2ReviewerVerification = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2AppraiserVerification = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2AppraiserVerification = new System.Workflow.Activities.StateInitializationActivity();
            this.edH2SelfEvaluation = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniH2SelfEvaluation = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiserH2Verification = new System.Workflow.Activities.EventDrivenActivity();
            this.H2AppraiserVerificationStateInitiation = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiseeH2InitialGoalSetting = new System.Workflow.Activities.EventDrivenActivity();
            this.AppraiseeH2InitialGoalSetting = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiseeGoalSetting = new System.Workflow.Activities.EventDrivenActivity();
            this.AppraiseeInitialGoalSetting = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiseeSignOff = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniAppraiseeSignOff = new System.Workflow.Activities.StateInitializationActivity();
            this.edHR = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniHR = new System.Workflow.Activities.StateInitializationActivity();
            this.edReviewerVerification = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniReviewerVerification = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiserEvaluation = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniAppraiserEvaluation = new System.Workflow.Activities.StateInitializationActivity();
            this.edSelfEvaluation = new System.Workflow.Activities.EventDrivenActivity();
            this.stateIniSelfEvaluation = new System.Workflow.Activities.StateInitializationActivity();
            this.edAppraiserVerification = new System.Workflow.Activities.EventDrivenActivity();
            this.AppraiserVerificationStateInitiation = new System.Workflow.Activities.StateInitializationActivity();
            this.edInitiation = new System.Workflow.Activities.EventDrivenActivity();
            this.H1Deactivation = new System.Workflow.Activities.StateActivity();
            this.H2EmptyState = new System.Workflow.Activities.StateActivity();
            this.H1EmptyState = new System.Workflow.Activities.StateActivity();
            this.H2HR = new System.Workflow.Activities.StateActivity();
            this.H2AppraiseeSignOffOrAppeal = new System.Workflow.Activities.StateActivity();
            this.H2ReviewerVerification = new System.Workflow.Activities.StateActivity();
            this.H2AppraiserEvaluation = new System.Workflow.Activities.StateActivity();
            this.H2SelfEvaluation = new System.Workflow.Activities.StateActivity();
            this.AppraiserH2GoalVerification = new System.Workflow.Activities.StateActivity();
            this.H2InitialGoalSetting = new System.Workflow.Activities.StateActivity();
            this.AppraiseeGoalSetting = new System.Workflow.Activities.StateActivity();
            this.AppraiseeSignOffOrAppeal = new System.Workflow.Activities.StateActivity();
            this.ClosedState = new System.Workflow.Activities.StateActivity();
            this.HR = new System.Workflow.Activities.StateActivity();
            this.ReviewerVerification = new System.Workflow.Activities.StateActivity();
            this.AppraiserEvaluation = new System.Workflow.Activities.StateActivity();
            this.SelfEvaluation = new System.Workflow.Activities.StateActivity();
            this.AppraiserGoalVerification = new System.Workflow.Activities.StateActivity();
            this.VFS_AppraisalWorkflowInitialState = new System.Workflow.Activities.StateActivity();
            // 
            // setStateActivity4
            // 
            this.setStateActivity4.Name = "setStateActivity4";
            this.setStateActivity4.TargetStateName = "AppraiserH2GoalVerification";
            // 
            // logToHistoryListActivity40
            // 
            this.logToHistoryListActivity40.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity40.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "VFS_AppraisalWorkflow";
            activitybind1.Path = "logToHistory1_HistoryDescription";
            activitybind2.Name = "VFS_AppraisalWorkflow";
            activitybind2.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity40.Name = "logToHistoryListActivity40";
            this.logToHistoryListActivity40.OtherData = "";
            this.logToHistoryListActivity40.UserId = -1;
            this.logToHistoryListActivity40.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.logToHistoryListActivity40.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // setStateActivity3
            // 
            this.setStateActivity3.Name = "setStateActivity3";
            this.setStateActivity3.TargetStateName = "H2SelfEvaluation";
            // 
            // logToHistoryListActivity39
            // 
            this.logToHistoryListActivity39.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity39.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity39.HistoryDescription = "";
            this.logToHistoryListActivity39.HistoryOutcome = "";
            this.logToHistoryListActivity39.Name = "logToHistoryListActivity39";
            this.logToHistoryListActivity39.OtherData = "";
            this.logToHistoryListActivity39.UserId = -1;
            // 
            // setStateActivity2
            // 
            this.setStateActivity2.Name = "setStateActivity2";
            this.setStateActivity2.TargetStateName = "AppraiserGoalVerification";
            // 
            // logToHistoryListActivity38
            // 
            this.logToHistoryListActivity38.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity38.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind3.Name = "VFS_AppraisalWorkflow";
            activitybind3.Path = "logToHistory1_HistoryDescription";
            activitybind4.Name = "VFS_AppraisalWorkflow";
            activitybind4.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity38.Name = "logToHistoryListActivity38";
            this.logToHistoryListActivity38.OtherData = "";
            this.logToHistoryListActivity38.UserId = -1;
            this.logToHistoryListActivity38.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.logToHistoryListActivity38.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "SelfEvaluation";
            // 
            // logToHistoryListActivity37
            // 
            this.logToHistoryListActivity37.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity37.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity37.HistoryDescription = "";
            this.logToHistoryListActivity37.HistoryOutcome = "";
            this.logToHistoryListActivity37.Name = "logToHistoryListActivity37";
            this.logToHistoryListActivity37.OtherData = "";
            this.logToHistoryListActivity37.UserId = -1;
            // 
            // setStateToH2AppraiserRevaluationHR
            // 
            this.setStateToH2AppraiserRevaluationHR.Name = "setStateToH2AppraiserRevaluationHR";
            this.setStateToH2AppraiserRevaluationHR.TargetStateName = "H2AppraiserEvaluation";
            // 
            // logToHistoryListActivity32
            // 
            this.logToHistoryListActivity32.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity32.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind5.Name = "VFS_AppraisalWorkflow";
            activitybind5.Path = "logToHistory1_HistoryDescription";
            activitybind6.Name = "VFS_AppraisalWorkflow";
            activitybind6.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity32.Name = "logToHistoryListActivity32";
            this.logToHistoryListActivity32.OtherData = "";
            this.logToHistoryListActivity32.UserId = -1;
            this.logToHistoryListActivity32.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.logToHistoryListActivity32.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // setStateToClosedState
            // 
            this.setStateToClosedState.Name = "setStateToClosedState";
            this.setStateToClosedState.TargetStateName = "ClosedState";
            // 
            // logToHistoryListActivity31
            // 
            this.logToHistoryListActivity31.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity31.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind7.Name = "VFS_AppraisalWorkflow";
            activitybind7.Path = "logToHistory1_HistoryDescription";
            activitybind8.Name = "VFS_AppraisalWorkflow";
            activitybind8.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity31.Name = "logToHistoryListActivity31";
            this.logToHistoryListActivity31.OtherData = "";
            this.logToHistoryListActivity31.UserId = -1;
            this.logToHistoryListActivity31.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.logToHistoryListActivity31.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // setStateH2HR
            // 
            this.setStateH2HR.Name = "setStateH2HR";
            this.setStateH2HR.TargetStateName = "H2HR";
            // 
            // logToHistoryListActivity29
            // 
            this.logToHistoryListActivity29.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity29.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind9.Name = "VFS_AppraisalWorkflow";
            activitybind9.Path = "logToHistory1_HistoryDescription";
            activitybind10.Name = "VFS_AppraisalWorkflow";
            activitybind10.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity29.Name = "logToHistoryListActivity29";
            this.logToHistoryListActivity29.OtherData = "";
            this.logToHistoryListActivity29.UserId = -1;
            this.logToHistoryListActivity29.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.logToHistoryListActivity29.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            // 
            // setStateToH2AppraiserEvaluation
            // 
            this.setStateToH2AppraiserEvaluation.Name = "setStateToH2AppraiserEvaluation";
            this.setStateToH2AppraiserEvaluation.TargetStateName = "H2AppraiserEvaluation";
            // 
            // logToHistoryListActivity28
            // 
            this.logToHistoryListActivity28.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity28.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind11.Name = "VFS_AppraisalWorkflow";
            activitybind11.Path = "logToHistory1_HistoryDescription";
            activitybind12.Name = "VFS_AppraisalWorkflow";
            activitybind12.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity28.Name = "logToHistoryListActivity28";
            this.logToHistoryListActivity28.OtherData = "";
            this.logToHistoryListActivity28.UserId = -1;
            this.logToHistoryListActivity28.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.logToHistoryListActivity28.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            // 
            // setStateH2GoalSetting
            // 
            this.setStateH2GoalSetting.Name = "setStateH2GoalSetting";
            this.setStateH2GoalSetting.TargetStateName = "H2EmptyState";
            // 
            // logToHistoryListActivity34
            // 
            this.logToHistoryListActivity34.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity34.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind13.Name = "VFS_AppraisalWorkflow";
            activitybind13.Path = "logToHistory1_HistoryDescription";
            activitybind14.Name = "VFS_AppraisalWorkflow";
            activitybind14.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity34.Name = "logToHistoryListActivity34";
            this.logToHistoryListActivity34.OtherData = "";
            this.logToHistoryListActivity34.UserId = -1;
            this.logToHistoryListActivity34.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.logToHistoryListActivity34.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            // 
            // setStateToH2SelfEvaluation
            // 
            this.setStateToH2SelfEvaluation.Name = "setStateToH2SelfEvaluation";
            this.setStateToH2SelfEvaluation.TargetStateName = "H2SelfEvaluation";
            // 
            // logToHistoryListActivity20
            // 
            this.logToHistoryListActivity20.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity20.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity20.HistoryDescription = "";
            this.logToHistoryListActivity20.HistoryOutcome = "";
            this.logToHistoryListActivity20.Name = "logToHistoryListActivity20";
            this.logToHistoryListActivity20.OtherData = "";
            this.logToHistoryListActivity20.UserId = -1;
            // 
            // setStateToCountryHR
            // 
            this.setStateToCountryHR.Name = "setStateToCountryHR";
            this.setStateToCountryHR.TargetStateName = "HR";
            // 
            // logToHistoryListActivity11
            // 
            this.logToHistoryListActivity11.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity11.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind15.Name = "VFS_AppraisalWorkflow";
            activitybind15.Path = "logToHistory1_HistoryDescription";
            activitybind16.Name = "VFS_AppraisalWorkflow";
            activitybind16.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity11.Name = "logToHistoryListActivity11";
            this.logToHistoryListActivity11.OtherData = "";
            this.logToHistoryListActivity11.UserId = -1;
            this.logToHistoryListActivity11.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            this.logToHistoryListActivity11.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // setStateToAppraiserRevaluation
            // 
            this.setStateToAppraiserRevaluation.Name = "setStateToAppraiserRevaluation";
            this.setStateToAppraiserRevaluation.TargetStateName = "AppraiserEvaluation";
            // 
            // logToHistoryListActivity10
            // 
            this.logToHistoryListActivity10.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity10.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind17.Name = "VFS_AppraisalWorkflow";
            activitybind17.Path = "logToHistory1_HistoryDescription";
            activitybind18.Name = "VFS_AppraisalWorkflow";
            activitybind18.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity10.Name = "logToHistoryListActivity10";
            this.logToHistoryListActivity10.OtherData = "";
            this.logToHistoryListActivity10.UserId = -1;
            this.logToHistoryListActivity10.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.logToHistoryListActivity10.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            // 
            // setStateToAppraiserRevaluationHR
            // 
            this.setStateToAppraiserRevaluationHR.Name = "setStateToAppraiserRevaluationHR";
            this.setStateToAppraiserRevaluationHR.TargetStateName = "AppraiserEvaluation";
            // 
            // logToHistoryListActivity14
            // 
            this.logToHistoryListActivity14.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity14.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind19.Name = "VFS_AppraisalWorkflow";
            activitybind19.Path = "logToHistory1_HistoryDescription";
            activitybind20.Name = "VFS_AppraisalWorkflow";
            activitybind20.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity14.Name = "logToHistoryListActivity14";
            this.logToHistoryListActivity14.OtherData = "";
            this.logToHistoryListActivity14.UserId = -1;
            this.logToHistoryListActivity14.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.logToHistoryListActivity14.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            // 
            // setStateToH2InitialGoalSetting
            // 
            this.setStateToH2InitialGoalSetting.Name = "setStateToH2InitialGoalSetting";
            this.setStateToH2InitialGoalSetting.TargetStateName = "H2InitialGoalSetting";
            // 
            // logToHistoryListActivity13
            // 
            this.logToHistoryListActivity13.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity13.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind21.Name = "VFS_AppraisalWorkflow";
            activitybind21.Path = "logToHistory1_HistoryDescription";
            activitybind22.Name = "VFS_AppraisalWorkflow";
            activitybind22.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity13.Name = "logToHistoryListActivity13";
            this.logToHistoryListActivity13.OtherData = "";
            this.logToHistoryListActivity13.UserId = -1;
            this.logToHistoryListActivity13.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.logToHistoryListActivity13.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            // 
            // setStateToH1GoalChanges
            // 
            this.setStateToH1GoalChanges.Name = "setStateToH1GoalChanges";
            this.setStateToH1GoalChanges.TargetStateName = "H1EmptyState";
            // 
            // logToHistoryListActivity2
            // 
            this.logToHistoryListActivity2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind23.Name = "VFS_AppraisalWorkflow";
            activitybind23.Path = "logToHistory1_HistoryDescription";
            activitybind24.Name = "VFS_AppraisalWorkflow";
            activitybind24.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity2.Name = "logToHistoryListActivity2";
            this.logToHistoryListActivity2.OtherData = "";
            this.logToHistoryListActivity2.UserId = -1;
            this.logToHistoryListActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            this.logToHistoryListActivity2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind24)));
            // 
            // setStateToSelfEvaluation
            // 
            this.setStateToSelfEvaluation.Name = "setStateToSelfEvaluation";
            this.setStateToSelfEvaluation.TargetStateName = "SelfEvaluation";
            // 
            // logToHistoryListActivity1
            // 
            this.logToHistoryListActivity1.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind25.Name = "VFS_AppraisalWorkflow";
            activitybind25.Path = "logToHistory1_HistoryDescription";
            activitybind26.Name = "VFS_AppraisalWorkflow";
            activitybind26.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity1.Name = "logToHistoryListActivity1";
            this.logToHistoryListActivity1.OtherData = "";
            this.logToHistoryListActivity1.UserId = -1;
            this.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind26)));
            this.logToHistoryListActivity1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind25)));
            // 
            // ifH2GoalsAmendment
            // 
            this.ifH2GoalsAmendment.Activities.Add(this.logToHistoryListActivity40);
            this.ifH2GoalsAmendment.Activities.Add(this.setStateActivity4);
            this.ifH2GoalsAmendment.Name = "ifH2GoalsAmendment";
            // 
            // ifH2TMTTrigger
            // 
            this.ifH2TMTTrigger.Activities.Add(this.logToHistoryListActivity39);
            this.ifH2TMTTrigger.Activities.Add(this.setStateActivity3);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2SelfEvaluationStarted);
            this.ifH2TMTTrigger.Condition = codecondition1;
            this.ifH2TMTTrigger.Name = "ifH2TMTTrigger";
            // 
            // ifGoalsAmendment
            // 
            this.ifGoalsAmendment.Activities.Add(this.logToHistoryListActivity38);
            this.ifGoalsAmendment.Activities.Add(this.setStateActivity2);
            this.ifGoalsAmendment.Name = "ifGoalsAmendment";
            // 
            // ifTMTTrigger
            // 
            this.ifTMTTrigger.Activities.Add(this.logToHistoryListActivity37);
            this.ifTMTTrigger.Activities.Add(this.setStateActivity1);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1SelfEvaluationStarted);
            this.ifTMTTrigger.Condition = codecondition2;
            this.ifTMTTrigger.Name = "ifTMTTrigger";
            // 
            // isH2CountryHRRequest
            // 
            this.isH2CountryHRRequest.Activities.Add(this.logToHistoryListActivity32);
            this.isH2CountryHRRequest.Activities.Add(this.setStateToH2AppraiserRevaluationHR);
            this.isH2CountryHRRequest.Name = "isH2CountryHRRequest";
            // 
            // isH2CountryHRClose
            // 
            this.isH2CountryHRClose.Activities.Add(this.logToHistoryListActivity31);
            this.isH2CountryHRClose.Activities.Add(this.setStateToClosedState);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2CountryHRClose);
            this.isH2CountryHRClose.Condition = codecondition3;
            this.isH2CountryHRClose.Name = "isH2CountryHRClose";
            // 
            // isH2AppraiseeSignedOff
            // 
            this.isH2AppraiseeSignedOff.Activities.Add(this.logToHistoryListActivity29);
            this.isH2AppraiseeSignedOff.Activities.Add(this.setStateH2HR);
            this.isH2AppraiseeSignedOff.Name = "isH2AppraiseeSignedOff";
            // 
            // isH2AppraiseeAppeal
            // 
            this.isH2AppraiseeAppeal.Activities.Add(this.logToHistoryListActivity28);
            this.isH2AppraiseeAppeal.Activities.Add(this.setStateToH2AppraiserEvaluation);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2AppraiseeAppeal);
            this.isH2AppraiseeAppeal.Condition = codecondition4;
            this.isH2AppraiseeAppeal.Name = "isH2AppraiseeAppeal";
            // 
            // ifElseH2TMTInitiationNotStarted
            // 
            this.ifElseH2TMTInitiationNotStarted.Activities.Add(this.logToHistoryListActivity34);
            this.ifElseH2TMTInitiationNotStarted.Activities.Add(this.setStateH2GoalSetting);
            this.ifElseH2TMTInitiationNotStarted.Name = "ifElseH2TMTInitiationNotStarted";
            // 
            // ifElseH2TMTInitiationStarted
            // 
            this.ifElseH2TMTInitiationStarted.Activities.Add(this.logToHistoryListActivity20);
            this.ifElseH2TMTInitiationStarted.Activities.Add(this.setStateToH2SelfEvaluation);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2TMTInitiates);
            this.ifElseH2TMTInitiationStarted.Condition = codecondition5;
            this.ifElseH2TMTInitiationStarted.Name = "ifElseH2TMTInitiationStarted";
            // 
            // isAppraiseeSignedOff
            // 
            this.isAppraiseeSignedOff.Activities.Add(this.logToHistoryListActivity11);
            this.isAppraiseeSignedOff.Activities.Add(this.setStateToCountryHR);
            this.isAppraiseeSignedOff.Name = "isAppraiseeSignedOff";
            // 
            // isAppraiseeAppeal
            // 
            this.isAppraiseeAppeal.Activities.Add(this.logToHistoryListActivity10);
            this.isAppraiseeAppeal.Activities.Add(this.setStateToAppraiserRevaluation);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.AppraiseeAppeal);
            this.isAppraiseeAppeal.Condition = codecondition6;
            this.isAppraiseeAppeal.Name = "isAppraiseeAppeal";
            // 
            // isCountryHRRequest
            // 
            this.isCountryHRRequest.Activities.Add(this.logToHistoryListActivity14);
            this.isCountryHRRequest.Activities.Add(this.setStateToAppraiserRevaluationHR);
            this.isCountryHRRequest.Name = "isCountryHRRequest";
            // 
            // isCountryHRClose
            // 
            this.isCountryHRClose.Activities.Add(this.logToHistoryListActivity13);
            this.isCountryHRClose.Activities.Add(this.setStateToH2InitialGoalSetting);
            codecondition7.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CountryHRClose);
            this.isCountryHRClose.Condition = codecondition7;
            this.isCountryHRClose.Name = "isCountryHRClose";
            // 
            // ifElseTMTInitiationNotStarted
            // 
            this.ifElseTMTInitiationNotStarted.Activities.Add(this.logToHistoryListActivity2);
            this.ifElseTMTInitiationNotStarted.Activities.Add(this.setStateToH1GoalChanges);
            this.ifElseTMTInitiationNotStarted.Name = "ifElseTMTInitiationNotStarted";
            // 
            // ifElseTMTInitiationStarted
            // 
            this.ifElseTMTInitiationStarted.Activities.Add(this.logToHistoryListActivity1);
            this.ifElseTMTInitiationStarted.Activities.Add(this.setStateToSelfEvaluation);
            codecondition8.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TMTInitiates);
            this.ifElseTMTInitiationStarted.Condition = codecondition8;
            this.ifElseTMTInitiationStarted.Name = "ifElseTMTInitiationStarted";
            // 
            // ifH2Evaluation
            // 
            this.ifH2Evaluation.Activities.Add(this.ifH2TMTTrigger);
            this.ifH2Evaluation.Activities.Add(this.ifH2GoalsAmendment);
            this.ifH2Evaluation.Name = "ifH2Evaluation";
            // 
            // terminateActivity4
            // 
            this.terminateActivity4.Name = "terminateActivity4";
            // 
            // ifH1Evaluation
            // 
            this.ifH1Evaluation.Activities.Add(this.ifTMTTrigger);
            this.ifH1Evaluation.Activities.Add(this.ifGoalsAmendment);
            this.ifH1Evaluation.Name = "ifH1Evaluation";
            // 
            // setStateActivity8
            // 
            this.setStateActivity8.Name = "setStateActivity8";
            this.setStateActivity8.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity45
            // 
            this.logToHistoryListActivity45.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity45.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity45.HistoryDescription = "";
            this.logToHistoryListActivity45.HistoryOutcome = "";
            this.logToHistoryListActivity45.Name = "logToHistoryListActivity45";
            this.logToHistoryListActivity45.OtherData = "";
            this.logToHistoryListActivity45.UserId = -1;
            // 
            // ifElseH2CountryHR
            // 
            this.ifElseH2CountryHR.Activities.Add(this.isH2CountryHRClose);
            this.ifElseH2CountryHR.Activities.Add(this.isH2CountryHRRequest);
            this.ifElseH2CountryHR.Name = "ifElseH2CountryHR";
            // 
            // terminateActivity8
            // 
            this.terminateActivity8.Name = "terminateActivity8";
            // 
            // ifElseH2AppraiseeAppeal
            // 
            this.ifElseH2AppraiseeAppeal.Activities.Add(this.isH2AppraiseeAppeal);
            this.ifElseH2AppraiseeAppeal.Activities.Add(this.isH2AppraiseeSignedOff);
            this.ifElseH2AppraiseeAppeal.Name = "ifElseH2AppraiseeAppeal";
            // 
            // terminateActivity7
            // 
            this.terminateActivity7.Name = "terminateActivity7";
            // 
            // setStateToH2AppraiseeSignOffOrAppeal
            // 
            this.setStateToH2AppraiseeSignOffOrAppeal.Name = "setStateToH2AppraiseeSignOffOrAppeal";
            this.setStateToH2AppraiseeSignOffOrAppeal.TargetStateName = "H2AppraiseeSignOffOrAppeal";
            // 
            // logToHistoryListActivity26
            // 
            this.logToHistoryListActivity26.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity26.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind27.Name = "VFS_AppraisalWorkflow";
            activitybind27.Path = "logToHistory1_HistoryDescription";
            activitybind28.Name = "VFS_AppraisalWorkflow";
            activitybind28.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity26.Name = "logToHistoryListActivity26";
            this.logToHistoryListActivity26.OtherData = "";
            this.logToHistoryListActivity26.UserId = -1;
            this.logToHistoryListActivity26.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind27)));
            this.logToHistoryListActivity26.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind28)));
            // 
            // terminateActivity6
            // 
            this.terminateActivity6.Name = "terminateActivity6";
            // 
            // setStateH2ReviewerVerification
            // 
            this.setStateH2ReviewerVerification.Name = "setStateH2ReviewerVerification";
            this.setStateH2ReviewerVerification.TargetStateName = "H2ReviewerVerification";
            // 
            // logToHistoryListActivity24
            // 
            this.logToHistoryListActivity24.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity24.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind29.Name = "VFS_AppraisalWorkflow";
            activitybind29.Path = "logToHistory1_HistoryDescription";
            activitybind30.Name = "VFS_AppraisalWorkflow";
            activitybind30.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity24.Name = "logToHistoryListActivity24";
            this.logToHistoryListActivity24.OtherData = "";
            this.logToHistoryListActivity24.UserId = -1;
            this.logToHistoryListActivity24.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind29)));
            this.logToHistoryListActivity24.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind30)));
            // 
            // terminateActivity5
            // 
            this.terminateActivity5.Name = "terminateActivity5";
            // 
            // setStateH2AppraiserEvaluation
            // 
            this.setStateH2AppraiserEvaluation.Name = "setStateH2AppraiserEvaluation";
            this.setStateH2AppraiserEvaluation.TargetStateName = "H2AppraiserEvaluation";
            // 
            // logToHistoryListActivity22
            // 
            this.logToHistoryListActivity22.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity22.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind31.Name = "VFS_AppraisalWorkflow";
            activitybind31.Path = "logToHistory1_HistoryDescription";
            activitybind32.Name = "VFS_AppraisalWorkflow";
            activitybind32.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity22.Name = "logToHistoryListActivity22";
            this.logToHistoryListActivity22.OtherData = "";
            this.logToHistoryListActivity22.UserId = -1;
            this.logToHistoryListActivity22.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind31)));
            this.logToHistoryListActivity22.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind32)));
            // 
            // terminateActivity3
            // 
            this.terminateActivity3.Name = "terminateActivity3";
            // 
            // ifElseH2TMTInitiation
            // 
            this.ifElseH2TMTInitiation.Activities.Add(this.ifElseH2TMTInitiationStarted);
            this.ifElseH2TMTInitiation.Activities.Add(this.ifElseH2TMTInitiationNotStarted);
            this.ifElseH2TMTInitiation.Name = "ifElseH2TMTInitiation";
            // 
            // caForH2TMTInitiation
            // 
            this.caForH2TMTInitiation.Name = "caForH2TMTInitiation";
            this.caForH2TMTInitiation.ExecuteCode += new System.EventHandler(this.caForTMTInitiation_ExecuteCodecaForH2TMTInitiation_ExecuteCode);
            // 
            // terminateActivity2
            // 
            this.terminateActivity2.Name = "terminateActivity2";
            // 
            // terminateActivity1
            // 
            this.terminateActivity1.Name = "terminateActivity1";
            // 
            // logToHistoryListActivity50
            // 
            this.logToHistoryListActivity50.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity50.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind33.Name = "VFS_AppraisalWorkflow";
            activitybind33.Path = "logToHistory1_HistoryDescription";
            activitybind34.Name = "VFS_AppraisalWorkflow";
            activitybind34.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity50.Name = "logToHistoryListActivity50";
            this.logToHistoryListActivity50.OtherData = "";
            this.logToHistoryListActivity50.UserId = -1;
            this.logToHistoryListActivity50.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind33)));
            this.logToHistoryListActivity50.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind34)));
            // 
            // setStateActivity5
            // 
            this.setStateActivity5.Name = "setStateActivity5";
            this.setStateActivity5.TargetStateName = "AppraiserH2GoalVerification";
            // 
            // logToHistoryListActivity18
            // 
            this.logToHistoryListActivity18.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity18.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind35.Name = "VFS_AppraisalWorkflow";
            activitybind35.Path = "logToHistory1_HistoryDescription";
            activitybind36.Name = "VFS_AppraisalWorkflow";
            activitybind36.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity18.Name = "logToHistoryListActivity18";
            this.logToHistoryListActivity18.OtherData = "";
            this.logToHistoryListActivity18.UserId = -1;
            this.logToHistoryListActivity18.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind35)));
            this.logToHistoryListActivity18.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind36)));
            // 
            // setSateToGoalVerification
            // 
            this.setSateToGoalVerification.Name = "setSateToGoalVerification";
            this.setSateToGoalVerification.TargetStateName = "AppraiserGoalVerification";
            // 
            // logToHistoryListActivity16
            // 
            this.logToHistoryListActivity16.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity16.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind37.Name = "VFS_AppraisalWorkflow";
            activitybind37.Path = "logToHistory1_HistoryDescription";
            activitybind38.Name = "VFS_AppraisalWorkflow";
            activitybind38.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity16.Name = "logToHistoryListActivity16";
            this.logToHistoryListActivity16.OtherData = "";
            this.logToHistoryListActivity16.UserId = -1;
            this.logToHistoryListActivity16.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind37)));
            this.logToHistoryListActivity16.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind38)));
            // 
            // setStateToDeactivation1
            // 
            this.setStateToDeactivation1.Name = "setStateToDeactivation1";
            this.setStateToDeactivation1.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity41
            // 
            this.logToHistoryListActivity41.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity41.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind39.Name = "VFS_AppraisalWorkflow";
            activitybind39.Path = "logToHistory1_HistoryDescription";
            activitybind40.Name = "VFS_AppraisalWorkflow";
            activitybind40.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity41.Name = "logToHistoryListActivity41";
            this.logToHistoryListActivity41.OtherData = "";
            this.logToHistoryListActivity41.UserId = -1;
            this.logToHistoryListActivity41.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind39)));
            this.logToHistoryListActivity41.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind40)));
            // 
            // ifElseAppraiseeAppeal
            // 
            this.ifElseAppraiseeAppeal.Activities.Add(this.isAppraiseeAppeal);
            this.ifElseAppraiseeAppeal.Activities.Add(this.isAppraiseeSignedOff);
            this.ifElseAppraiseeAppeal.Name = "ifElseAppraiseeAppeal";
            // 
            // setStateActivity11
            // 
            this.setStateActivity11.Name = "setStateActivity11";
            this.setStateActivity11.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity48
            // 
            this.logToHistoryListActivity48.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity48.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind41.Name = "VFS_AppraisalWorkflow";
            activitybind41.Path = "logToHistory1_HistoryDescription";
            activitybind42.Name = "VFS_AppraisalWorkflow";
            activitybind42.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity48.Name = "logToHistoryListActivity48";
            this.logToHistoryListActivity48.OtherData = "";
            this.logToHistoryListActivity48.UserId = -1;
            this.logToHistoryListActivity48.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind41)));
            this.logToHistoryListActivity48.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind42)));
            // 
            // ifElseCountryHR
            // 
            this.ifElseCountryHR.Activities.Add(this.isCountryHRClose);
            this.ifElseCountryHR.Activities.Add(this.isCountryHRRequest);
            this.ifElseCountryHR.Name = "ifElseCountryHR";
            // 
            // setStateActivity12
            // 
            this.setStateActivity12.Name = "setStateActivity12";
            this.setStateActivity12.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity49
            // 
            this.logToHistoryListActivity49.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity49.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind43.Name = "VFS_AppraisalWorkflow";
            activitybind43.Path = "logToHistory1_HistoryDescription";
            activitybind44.Name = "VFS_AppraisalWorkflow";
            activitybind44.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity49.Name = "logToHistoryListActivity49";
            this.logToHistoryListActivity49.OtherData = "";
            this.logToHistoryListActivity49.UserId = -1;
            this.logToHistoryListActivity49.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind43)));
            this.logToHistoryListActivity49.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind44)));
            // 
            // setStateToAppraiseeSignOff
            // 
            this.setStateToAppraiseeSignOff.Name = "setStateToAppraiseeSignOff";
            this.setStateToAppraiseeSignOff.TargetStateName = "AppraiseeSignOffOrAppeal";
            // 
            // logToHistoryListActivity8
            // 
            this.logToHistoryListActivity8.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity8.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind45.Name = "VFS_AppraisalWorkflow";
            activitybind45.Path = "logToHistory1_HistoryDescription";
            activitybind46.Name = "VFS_AppraisalWorkflow";
            activitybind46.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity8.Name = "logToHistoryListActivity8";
            this.logToHistoryListActivity8.OtherData = "";
            this.logToHistoryListActivity8.UserId = -1;
            this.logToHistoryListActivity8.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind45)));
            this.logToHistoryListActivity8.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind46)));
            // 
            // setStateActivity10
            // 
            this.setStateActivity10.Name = "setStateActivity10";
            this.setStateActivity10.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity47
            // 
            this.logToHistoryListActivity47.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity47.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind47.Name = "VFS_AppraisalWorkflow";
            activitybind47.Path = "logToHistory1_HistoryDescription";
            activitybind48.Name = "VFS_AppraisalWorkflow";
            activitybind48.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity47.Name = "logToHistoryListActivity47";
            this.logToHistoryListActivity47.OtherData = "";
            this.logToHistoryListActivity47.UserId = -1;
            this.logToHistoryListActivity47.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind47)));
            this.logToHistoryListActivity47.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind48)));
            // 
            // setStateToReviewerVerification
            // 
            this.setStateToReviewerVerification.Name = "setStateToReviewerVerification";
            this.setStateToReviewerVerification.TargetStateName = "ReviewerVerification";
            // 
            // logToHistoryListActivity6
            // 
            this.logToHistoryListActivity6.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity6.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind49.Name = "VFS_AppraisalWorkflow";
            activitybind49.Path = "logToHistory1_HistoryDescription";
            activitybind50.Name = "VFS_AppraisalWorkflow";
            activitybind50.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity6.Name = "logToHistoryListActivity6";
            this.logToHistoryListActivity6.OtherData = "";
            this.logToHistoryListActivity6.UserId = -1;
            this.logToHistoryListActivity6.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind49)));
            this.logToHistoryListActivity6.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind50)));
            // 
            // setStateActivity9
            // 
            this.setStateActivity9.Name = "setStateActivity9";
            this.setStateActivity9.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity46
            // 
            this.logToHistoryListActivity46.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity46.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind51.Name = "VFS_AppraisalWorkflow";
            activitybind51.Path = "logToHistory1_HistoryDescription";
            activitybind52.Name = "VFS_AppraisalWorkflow";
            activitybind52.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity46.Name = "logToHistoryListActivity46";
            this.logToHistoryListActivity46.OtherData = "";
            this.logToHistoryListActivity46.UserId = -1;
            this.logToHistoryListActivity46.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind51)));
            this.logToHistoryListActivity46.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind52)));
            // 
            // setStateToAppraiserEvaluation
            // 
            this.setStateToAppraiserEvaluation.Name = "setStateToAppraiserEvaluation";
            this.setStateToAppraiserEvaluation.TargetStateName = "AppraiserEvaluation";
            // 
            // logToHistoryListActivity4
            // 
            this.logToHistoryListActivity4.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity4.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind53.Name = "VFS_AppraisalWorkflow";
            activitybind53.Path = "logToHistory1_HistoryDescription";
            activitybind54.Name = "VFS_AppraisalWorkflow";
            activitybind54.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity4.Name = "logToHistoryListActivity4";
            this.logToHistoryListActivity4.OtherData = "";
            this.logToHistoryListActivity4.UserId = -1;
            this.logToHistoryListActivity4.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind53)));
            this.logToHistoryListActivity4.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind54)));
            // 
            // setStateActivity7
            // 
            this.setStateActivity7.Name = "setStateActivity7";
            this.setStateActivity7.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity44
            // 
            this.logToHistoryListActivity44.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity44.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind55.Name = "VFS_AppraisalWorkflow";
            activitybind55.Path = "logToHistory1_HistoryDescription";
            activitybind56.Name = "VFS_AppraisalWorkflow";
            activitybind56.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity44.Name = "logToHistoryListActivity44";
            this.logToHistoryListActivity44.OtherData = "";
            this.logToHistoryListActivity44.UserId = -1;
            this.logToHistoryListActivity44.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind55)));
            this.logToHistoryListActivity44.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind56)));
            // 
            // ifElseTMTInitiation
            // 
            this.ifElseTMTInitiation.Activities.Add(this.ifElseTMTInitiationStarted);
            this.ifElseTMTInitiation.Activities.Add(this.ifElseTMTInitiationNotStarted);
            this.ifElseTMTInitiation.Name = "ifElseTMTInitiation";
            // 
            // caForTMTInitiation
            // 
            this.caForTMTInitiation.Name = "caForTMTInitiation";
            this.caForTMTInitiation.ExecuteCode += new System.EventHandler(this.caForTMTInitiation_ExecuteCode);
            // 
            // setStateActivity6
            // 
            this.setStateActivity6.Name = "setStateActivity6";
            this.setStateActivity6.TargetStateName = "H1Deactivation";
            // 
            // logToHistoryListActivity43
            // 
            this.logToHistoryListActivity43.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity43.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind57.Name = "VFS_AppraisalWorkflow";
            activitybind57.Path = "logToHistory1_HistoryDescription";
            activitybind58.Name = "VFS_AppraisalWorkflow";
            activitybind58.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity43.Name = "logToHistoryListActivity43";
            this.logToHistoryListActivity43.OtherData = "";
            this.logToHistoryListActivity43.UserId = -1;
            this.logToHistoryListActivity43.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind57)));
            this.logToHistoryListActivity43.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind58)));
            // 
            // setStateToAppraiseeGoalSetting
            // 
            this.setStateToAppraiseeGoalSetting.Name = "setStateToAppraiseeGoalSetting";
            this.setStateToAppraiseeGoalSetting.TargetStateName = "AppraiseeGoalSetting";
            // 
            // setStateToH2GoalSetting
            // 
            this.setStateToH2GoalSetting.Name = "setStateToH2GoalSetting";
            this.setStateToH2GoalSetting.TargetStateName = "H2InitialGoalSetting";
            // 
            // ifElseBranchActivity12
            // 
            this.ifElseBranchActivity12.Activities.Add(this.ifH2Evaluation);
            this.ifElseBranchActivity12.Name = "ifElseBranchActivity12";
            // 
            // ifH2EmpttyDeactivation
            // 
            this.ifH2EmpttyDeactivation.Activities.Add(this.terminateActivity4);
            codecondition9.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2EmptyDeactivation);
            this.ifH2EmpttyDeactivation.Condition = codecondition9;
            this.ifH2EmpttyDeactivation.Name = "ifH2EmpttyDeactivation";
            // 
            // ifElseBranchActivity4
            // 
            this.ifElseBranchActivity4.Activities.Add(this.ifH1Evaluation);
            this.ifElseBranchActivity4.Name = "ifElseBranchActivity4";
            // 
            // ifH1EmptyDeactivation
            // 
            this.ifH1EmptyDeactivation.Activities.Add(this.logToHistoryListActivity45);
            this.ifH1EmptyDeactivation.Activities.Add(this.setStateActivity8);
            codecondition10.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1EmptyDeactivation);
            this.ifH1EmptyDeactivation.Condition = codecondition10;
            this.ifH1EmptyDeactivation.Name = "ifH1EmptyDeactivation";
            // 
            // ifElseBranchActivity17
            // 
            this.ifElseBranchActivity17.Activities.Add(this.ifElseH2CountryHR);
            this.ifElseBranchActivity17.Name = "ifElseBranchActivity17";
            // 
            // ifH2HRDeactivation
            // 
            this.ifH2HRDeactivation.Activities.Add(this.terminateActivity8);
            codecondition11.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2HRDeactivation);
            this.ifH2HRDeactivation.Condition = codecondition11;
            this.ifH2HRDeactivation.Name = "ifH2HRDeactivation";
            // 
            // ifElseBranchActivity16
            // 
            this.ifElseBranchActivity16.Activities.Add(this.ifElseH2AppraiseeAppeal);
            this.ifElseBranchActivity16.Name = "ifElseBranchActivity16";
            // 
            // ifH2SignOffDeactivation
            // 
            this.ifH2SignOffDeactivation.Activities.Add(this.terminateActivity7);
            codecondition12.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2SignOffDeactivation);
            this.ifH2SignOffDeactivation.Condition = codecondition12;
            this.ifH2SignOffDeactivation.Name = "ifH2SignOffDeactivation";
            // 
            // ifElseBranchActivity15
            // 
            this.ifElseBranchActivity15.Activities.Add(this.logToHistoryListActivity26);
            this.ifElseBranchActivity15.Activities.Add(this.setStateToH2AppraiseeSignOffOrAppeal);
            this.ifElseBranchActivity15.Name = "ifElseBranchActivity15";
            // 
            // ifH2RvrDeactivation
            // 
            this.ifH2RvrDeactivation.Activities.Add(this.terminateActivity6);
            codecondition13.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2ReviewerDeactivation);
            this.ifH2RvrDeactivation.Condition = codecondition13;
            this.ifH2RvrDeactivation.Name = "ifH2RvrDeactivation";
            // 
            // ifElseBranchActivity13
            // 
            this.ifElseBranchActivity13.Activities.Add(this.logToHistoryListActivity24);
            this.ifElseBranchActivity13.Activities.Add(this.setStateH2ReviewerVerification);
            this.ifElseBranchActivity13.Name = "ifElseBranchActivity13";
            // 
            // ifElseBranchActivity1
            // 
            this.ifElseBranchActivity1.Activities.Add(this.terminateActivity5);
            codecondition14.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2AppraiserEvalDeactivation);
            this.ifElseBranchActivity1.Condition = codecondition14;
            this.ifElseBranchActivity1.Name = "ifElseBranchActivity1";
            // 
            // ifElseBranchActivity11
            // 
            this.ifElseBranchActivity11.Activities.Add(this.logToHistoryListActivity22);
            this.ifElseBranchActivity11.Activities.Add(this.setStateH2AppraiserEvaluation);
            this.ifElseBranchActivity11.Name = "ifElseBranchActivity11";
            // 
            // ifH2SelfDeactivation
            // 
            this.ifH2SelfDeactivation.Activities.Add(this.terminateActivity3);
            codecondition15.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2SelfDeactivation);
            this.ifH2SelfDeactivation.Condition = codecondition15;
            this.ifH2SelfDeactivation.Name = "ifH2SelfDeactivation";
            // 
            // ifElseBranchActivity10
            // 
            this.ifElseBranchActivity10.Activities.Add(this.caForH2TMTInitiation);
            this.ifElseBranchActivity10.Activities.Add(this.ifElseH2TMTInitiation);
            this.ifElseBranchActivity10.Name = "ifElseBranchActivity10";
            // 
            // ifH2AppraiserDeactivation
            // 
            this.ifH2AppraiserDeactivation.Activities.Add(this.terminateActivity2);
            codecondition16.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H2AppraiserDeactivation);
            this.ifH2AppraiserDeactivation.Condition = codecondition16;
            this.ifH2AppraiserDeactivation.Name = "ifH2AppraiserDeactivation";
            // 
            // ifElseBranchActivity9
            // 
            this.ifElseBranchActivity9.Activities.Add(this.logToHistoryListActivity50);
            this.ifElseBranchActivity9.Activities.Add(this.terminateActivity1);
            this.ifElseBranchActivity9.Name = "ifElseBranchActivity9";
            // 
            // ifH2GoalsCompleted
            // 
            this.ifH2GoalsCompleted.Activities.Add(this.logToHistoryListActivity18);
            this.ifH2GoalsCompleted.Activities.Add(this.setStateActivity5);
            codecondition17.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.IfH1GoalsCompleted);
            this.ifH2GoalsCompleted.Condition = codecondition17;
            this.ifH2GoalsCompleted.Name = "ifH2GoalsCompleted";
            // 
            // ifH1GoalSettingComplete
            // 
            this.ifH1GoalSettingComplete.Activities.Add(this.logToHistoryListActivity16);
            this.ifH1GoalSettingComplete.Activities.Add(this.setSateToGoalVerification);
            this.ifH1GoalSettingComplete.Name = "ifH1GoalSettingComplete";
            // 
            // ifH1Deactivated
            // 
            this.ifH1Deactivated.Activities.Add(this.logToHistoryListActivity41);
            this.ifH1Deactivated.Activities.Add(this.setStateToDeactivation1);
            codecondition18.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1AppraiseeDeactivation);
            this.ifH1Deactivated.Condition = codecondition18;
            this.ifH1Deactivated.Name = "ifH1Deactivated";
            // 
            // ifElseBranchActivity7
            // 
            this.ifElseBranchActivity7.Activities.Add(this.ifElseAppraiseeAppeal);
            this.ifElseBranchActivity7.Name = "ifElseBranchActivity7";
            // 
            // ifH1AppraiseeSignOffDeactivation
            // 
            this.ifH1AppraiseeSignOffDeactivation.Activities.Add(this.logToHistoryListActivity48);
            this.ifH1AppraiseeSignOffDeactivation.Activities.Add(this.setStateActivity11);
            codecondition19.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1AppraiseeSignOffDeactivation);
            this.ifH1AppraiseeSignOffDeactivation.Condition = codecondition19;
            this.ifH1AppraiseeSignOffDeactivation.Name = "ifH1AppraiseeSignOffDeactivation";
            // 
            // ifElseBranchActivity8
            // 
            this.ifElseBranchActivity8.Activities.Add(this.ifElseCountryHR);
            this.ifElseBranchActivity8.Name = "ifElseBranchActivity8";
            // 
            // ifH1HRDeactivation
            // 
            this.ifH1HRDeactivation.Activities.Add(this.logToHistoryListActivity49);
            this.ifH1HRDeactivation.Activities.Add(this.setStateActivity12);
            codecondition20.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1HrDeactivation);
            this.ifH1HRDeactivation.Condition = codecondition20;
            this.ifH1HRDeactivation.Name = "ifH1HRDeactivation";
            // 
            // ifElseBranchActivity6
            // 
            this.ifElseBranchActivity6.Activities.Add(this.logToHistoryListActivity8);
            this.ifElseBranchActivity6.Activities.Add(this.setStateToAppraiseeSignOff);
            this.ifElseBranchActivity6.Name = "ifElseBranchActivity6";
            // 
            // ifRvrEvalDeactivation
            // 
            this.ifRvrEvalDeactivation.Activities.Add(this.logToHistoryListActivity47);
            this.ifRvrEvalDeactivation.Activities.Add(this.setStateActivity10);
            codecondition21.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1RvrEvalDeactivation);
            this.ifRvrEvalDeactivation.Condition = codecondition21;
            this.ifRvrEvalDeactivation.Name = "ifRvrEvalDeactivation";
            // 
            // ifElseBranchActivity5
            // 
            this.ifElseBranchActivity5.Activities.Add(this.logToHistoryListActivity6);
            this.ifElseBranchActivity5.Activities.Add(this.setStateToReviewerVerification);
            this.ifElseBranchActivity5.Name = "ifElseBranchActivity5";
            // 
            // ifH1AppraiserEvalDeactivated
            // 
            this.ifH1AppraiserEvalDeactivated.Activities.Add(this.logToHistoryListActivity46);
            this.ifH1AppraiserEvalDeactivated.Activities.Add(this.setStateActivity9);
            codecondition22.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1AppraiserEvalDeactivation);
            this.ifH1AppraiserEvalDeactivated.Condition = codecondition22;
            this.ifH1AppraiserEvalDeactivated.Name = "ifH1AppraiserEvalDeactivated";
            // 
            // ifElseBranchActivity2
            // 
            this.ifElseBranchActivity2.Activities.Add(this.logToHistoryListActivity4);
            this.ifElseBranchActivity2.Activities.Add(this.setStateToAppraiserEvaluation);
            this.ifElseBranchActivity2.Name = "ifElseBranchActivity2";
            // 
            // ifH1SelfDeactivation
            // 
            this.ifH1SelfDeactivation.Activities.Add(this.logToHistoryListActivity44);
            this.ifH1SelfDeactivation.Activities.Add(this.setStateActivity7);
            codecondition23.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1SelfDeactivation);
            this.ifH1SelfDeactivation.Condition = codecondition23;
            this.ifH1SelfDeactivation.Name = "ifH1SelfDeactivation";
            // 
            // ifElseBranchActivity3
            // 
            this.ifElseBranchActivity3.Activities.Add(this.caForTMTInitiation);
            this.ifElseBranchActivity3.Activities.Add(this.ifElseTMTInitiation);
            this.ifElseBranchActivity3.Name = "ifElseBranchActivity3";
            // 
            // ifH1AppraiserDeactivation
            // 
            this.ifH1AppraiserDeactivation.Activities.Add(this.logToHistoryListActivity43);
            this.ifH1AppraiserDeactivation.Activities.Add(this.setStateActivity6);
            codecondition24.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.H1AppraiserDeactivation);
            this.ifH1AppraiserDeactivation.Condition = codecondition24;
            this.ifH1AppraiserDeactivation.Name = "ifH1AppraiserDeactivation";
            // 
            // ifH2NotStarted
            // 
            this.ifH2NotStarted.Activities.Add(this.setStateToAppraiseeGoalSetting);
            this.ifH2NotStarted.Name = "ifH2NotStarted";
            // 
            // ifH2Started
            // 
            this.ifH2Started.Activities.Add(this.setStateToH2GoalSetting);
            codecondition25.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.CheckH2Started);
            this.ifH2Started.Condition = codecondition25;
            this.ifH2Started.Name = "ifH2Started";
            // 
            // setStateToH2Start
            // 
            this.setStateToH2Start.Name = "setStateToH2Start";
            this.setStateToH2Start.TargetStateName = "H2InitialGoalSetting";
            // 
            // logToHistoryListActivity42
            // 
            this.logToHistoryListActivity42.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity42.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind59.Name = "VFS_AppraisalWorkflow";
            activitybind59.Path = "logToHistory1_HistoryDescription";
            activitybind60.Name = "VFS_AppraisalWorkflow";
            activitybind60.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity42.Name = "logToHistoryListActivity42";
            this.logToHistoryListActivity42.OtherData = "";
            this.logToHistoryListActivity42.UserId = -1;
            this.logToHistoryListActivity42.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind59)));
            this.logToHistoryListActivity42.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind60)));
            // 
            // onTaskChangedH1Deactivation
            // 
            activitybind61.Name = "VFS_AppraisalWorkflow";
            activitybind61.Path = "onTaskChangedH1Deactivation_AfterProperties";
            activitybind62.Name = "VFS_AppraisalWorkflow";
            activitybind62.Path = "onTaskChangedH1Deactivation_BeforeProperties";
            correlationtoken1.Name = "ctH1Deactivation";
            correlationtoken1.OwnerActivityName = "H1Deactivation";
            this.onTaskChangedH1Deactivation.CorrelationToken = correlationtoken1;
            this.onTaskChangedH1Deactivation.Executor = null;
            this.onTaskChangedH1Deactivation.Name = "onTaskChangedH1Deactivation";
            activitybind63.Name = "VFS_AppraisalWorkflow";
            activitybind63.Path = "createTaskForDeactivation_TaskId";
            this.onTaskChangedH1Deactivation.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedH1Deactivation_Invoked);
            this.onTaskChangedH1Deactivation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind62)));
            this.onTaskChangedH1Deactivation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind63)));
            this.onTaskChangedH1Deactivation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind61)));
            // 
            // logToHistoryListActivity35
            // 
            this.logToHistoryListActivity35.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity35.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind64.Name = "VFS_AppraisalWorkflow";
            activitybind64.Path = "logToHistory1_HistoryDescription";
            activitybind65.Name = "VFS_AppraisalWorkflow";
            activitybind65.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity35.Name = "logToHistoryListActivity35";
            this.logToHistoryListActivity35.OtherData = "";
            this.logToHistoryListActivity35.UserId = -1;
            this.logToHistoryListActivity35.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind64)));
            this.logToHistoryListActivity35.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind65)));
            // 
            // createTaskForDeactivation
            // 
            this.createTaskForDeactivation.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForDeactivation.CorrelationToken = correlationtoken1;
            this.createTaskForDeactivation.ListItemId = -1;
            this.createTaskForDeactivation.Name = "createTaskForDeactivation";
            this.createTaskForDeactivation.SpecialPermissions = null;
            activitybind66.Name = "VFS_AppraisalWorkflow";
            activitybind66.Path = "createTaskForDeactivation_TaskId";
            activitybind67.Name = "VFS_AppraisalWorkflow";
            activitybind67.Path = "createTaskForDeactivation_TaskProperties";
            this.createTaskForDeactivation.MethodInvoking += new System.EventHandler(this.createTaskForDeactivation_MethodInvoking);
            this.createTaskForDeactivation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind67)));
            this.createTaskForDeactivation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind66)));
            // 
            // ifElseH2EmptyDeactivation
            // 
            this.ifElseH2EmptyDeactivation.Activities.Add(this.ifH2EmpttyDeactivation);
            this.ifElseH2EmptyDeactivation.Activities.Add(this.ifElseBranchActivity12);
            this.ifElseH2EmptyDeactivation.Name = "ifElseH2EmptyDeactivation";
            // 
            // onH2EmptyTaskChanged
            // 
            activitybind68.Name = "VFS_AppraisalWorkflow";
            activitybind68.Path = "onH2EmptyTaskChanged_AfterProperties";
            activitybind69.Name = "VFS_AppraisalWorkflow";
            activitybind69.Path = "onH2EmptyTaskChanged_BeforeProperties";
            correlationtoken2.Name = "ctH2Empty";
            correlationtoken2.OwnerActivityName = "H2EmptyState";
            this.onH2EmptyTaskChanged.CorrelationToken = correlationtoken2;
            this.onH2EmptyTaskChanged.Executor = null;
            this.onH2EmptyTaskChanged.Name = "onH2EmptyTaskChanged";
            activitybind70.Name = "VFS_AppraisalWorkflow";
            activitybind70.Path = "createTaskForH1Empty_TaskId";
            this.onH2EmptyTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onH2EmptyTaskChanged_Invoked);
            this.onH2EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind69)));
            this.onH2EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind70)));
            this.onH2EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind68)));
            // 
            // logToHistoryListActivity33
            // 
            this.logToHistoryListActivity33.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity33.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind71.Name = "VFS_AppraisalWorkflow";
            activitybind71.Path = "logToHistory1_HistoryDescription";
            activitybind72.Name = "VFS_AppraisalWorkflow";
            activitybind72.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity33.Name = "logToHistoryListActivity33";
            this.logToHistoryListActivity33.OtherData = "";
            this.logToHistoryListActivity33.UserId = -1;
            this.logToHistoryListActivity33.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind71)));
            this.logToHistoryListActivity33.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind72)));
            // 
            // createTaskForH2Empty
            // 
            this.createTaskForH2Empty.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2Empty.CorrelationToken = correlationtoken2;
            this.createTaskForH2Empty.ListItemId = -1;
            this.createTaskForH2Empty.Name = "createTaskForH2Empty";
            this.createTaskForH2Empty.SpecialPermissions = null;
            activitybind73.Name = "VFS_AppraisalWorkflow";
            activitybind73.Path = "createTaskForH2Empty_TaskId";
            activitybind74.Name = "VFS_AppraisalWorkflow";
            activitybind74.Path = "createTaskForH2Empty_TaskProperties";
            this.createTaskForH2Empty.MethodInvoking += new System.EventHandler(this.createTaskForH2Empty_MethodInvoking);
            this.createTaskForH2Empty.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind74)));
            this.createTaskForH2Empty.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind73)));
            // 
            // ifElseH1Empty
            // 
            this.ifElseH1Empty.Activities.Add(this.ifH1EmptyDeactivation);
            this.ifElseH1Empty.Activities.Add(this.ifElseBranchActivity4);
            this.ifElseH1Empty.Name = "ifElseH1Empty";
            // 
            // onH1EmptyTaskChanged
            // 
            activitybind75.Name = "VFS_AppraisalWorkflow";
            activitybind75.Path = "onH1EmptyTaskChanged_AfterProperties";
            activitybind76.Name = "VFS_AppraisalWorkflow";
            activitybind76.Path = "onH1EmptyTaskChanged_BeforeProperties";
            correlationtoken3.Name = "ctH1Empty";
            correlationtoken3.OwnerActivityName = "H1EmptyState";
            this.onH1EmptyTaskChanged.CorrelationToken = correlationtoken3;
            this.onH1EmptyTaskChanged.Executor = null;
            this.onH1EmptyTaskChanged.Name = "onH1EmptyTaskChanged";
            activitybind77.Name = "VFS_AppraisalWorkflow";
            activitybind77.Path = "createTaskForH1Empty_TaskId";
            this.onH1EmptyTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onH1EmptyTaskChanged_Invoked);
            this.onH1EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind75)));
            this.onH1EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind76)));
            this.onH1EmptyTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind77)));
            // 
            // logToHistoryListActivity36
            // 
            this.logToHistoryListActivity36.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity36.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind78.Name = "VFS_AppraisalWorkflow";
            activitybind78.Path = "logToHistory1_HistoryDescription";
            activitybind79.Name = "VFS_AppraisalWorkflow";
            activitybind79.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity36.Name = "logToHistoryListActivity36";
            this.logToHistoryListActivity36.OtherData = "";
            this.logToHistoryListActivity36.UserId = -1;
            this.logToHistoryListActivity36.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind78)));
            this.logToHistoryListActivity36.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind79)));
            // 
            // createTaskForH1Empty
            // 
            this.createTaskForH1Empty.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH1Empty.CorrelationToken = correlationtoken3;
            this.createTaskForH1Empty.ListItemId = -1;
            this.createTaskForH1Empty.Name = "createTaskForH1Empty";
            this.createTaskForH1Empty.SpecialPermissions = null;
            activitybind80.Name = "VFS_AppraisalWorkflow";
            activitybind80.Path = "createTaskForH1Empty_TaskId";
            activitybind81.Name = "VFS_AppraisalWorkflow";
            activitybind81.Path = "createTaskForH1Empty_TaskProperties";
            this.createTaskForH1Empty.MethodInvoking += new System.EventHandler(this.createTaskForH1Empty_MethodInvoking);
            this.createTaskForH1Empty.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind80)));
            this.createTaskForH1Empty.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind81)));
            // 
            // ifElseH2HRDeactivation
            // 
            this.ifElseH2HRDeactivation.Activities.Add(this.ifH2HRDeactivation);
            this.ifElseH2HRDeactivation.Activities.Add(this.ifElseBranchActivity17);
            this.ifElseH2HRDeactivation.Name = "ifElseH2HRDeactivation";
            // 
            // onTaskChangedForH2CountryHR
            // 
            activitybind82.Name = "VFS_AppraisalWorkflow";
            activitybind82.Path = "onTaskChangedForH2CountryHR_AfterProperties";
            activitybind83.Name = "VFS_AppraisalWorkflow";
            activitybind83.Path = "onTaskChangedForH2CountryHR_BeforeProperties";
            correlationtoken4.Name = "ctH2CountryHR";
            correlationtoken4.OwnerActivityName = "H2HR";
            this.onTaskChangedForH2CountryHR.CorrelationToken = correlationtoken4;
            this.onTaskChangedForH2CountryHR.Executor = null;
            this.onTaskChangedForH2CountryHR.Name = "onTaskChangedForH2CountryHR";
            activitybind84.Name = "VFS_AppraisalWorkflow";
            activitybind84.Path = "createTaskForH2CountryHR_TaskId";
            this.onTaskChangedForH2CountryHR.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForH2CountryHR_Invoked);
            this.onTaskChangedForH2CountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind83)));
            this.onTaskChangedForH2CountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind84)));
            this.onTaskChangedForH2CountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind82)));
            // 
            // logToHistoryListActivity30
            // 
            this.logToHistoryListActivity30.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity30.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind85.Name = "VFS_AppraisalWorkflow";
            activitybind85.Path = "logToHistory1_HistoryDescription";
            activitybind86.Name = "VFS_AppraisalWorkflow";
            activitybind86.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity30.Name = "logToHistoryListActivity30";
            this.logToHistoryListActivity30.OtherData = "";
            this.logToHistoryListActivity30.UserId = -1;
            this.logToHistoryListActivity30.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind85)));
            this.logToHistoryListActivity30.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind86)));
            // 
            // createTaskForH2CountryHR
            // 
            this.createTaskForH2CountryHR.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken5.Name = "ctH2CountryHR";
            correlationtoken5.OwnerActivityName = "H2HR";
            this.createTaskForH2CountryHR.CorrelationToken = correlationtoken5;
            this.createTaskForH2CountryHR.ListItemId = -1;
            this.createTaskForH2CountryHR.Name = "createTaskForH2CountryHR";
            this.createTaskForH2CountryHR.SpecialPermissions = null;
            activitybind87.Name = "VFS_AppraisalWorkflow";
            activitybind87.Path = "createTaskForH2CountryHR_TaskId";
            activitybind88.Name = "VFS_AppraisalWorkflow";
            activitybind88.Path = "createTaskForH2CountryHR_TaskProperties";
            this.createTaskForH2CountryHR.MethodInvoking += new System.EventHandler(this.createTaskForH2CountryHR_MethodInvoking);
            this.createTaskForH2CountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind88)));
            this.createTaskForH2CountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind87)));
            // 
            // ifElseH2AppraiseeSignOff
            // 
            this.ifElseH2AppraiseeSignOff.Activities.Add(this.ifH2SignOffDeactivation);
            this.ifElseH2AppraiseeSignOff.Activities.Add(this.ifElseBranchActivity16);
            this.ifElseH2AppraiseeSignOff.Name = "ifElseH2AppraiseeSignOff";
            // 
            // onTaskChangedForH2AppraiseeSignOff
            // 
            activitybind89.Name = "VFS_AppraisalWorkflow";
            activitybind89.Path = "onTaskChangedForH2AppraiseeSignOff_AfterProperties";
            activitybind90.Name = "VFS_AppraisalWorkflow";
            activitybind90.Path = "onTaskChangedForH2AppraiseeSignOff_BeforeProperties";
            correlationtoken6.Name = "ctH2AppraiseeSignOff";
            correlationtoken6.OwnerActivityName = "H2AppraiseeSignOffOrAppeal";
            this.onTaskChangedForH2AppraiseeSignOff.CorrelationToken = correlationtoken6;
            this.onTaskChangedForH2AppraiseeSignOff.Executor = null;
            this.onTaskChangedForH2AppraiseeSignOff.Name = "onTaskChangedForH2AppraiseeSignOff";
            activitybind91.Name = "VFS_AppraisalWorkflow";
            activitybind91.Path = "createTaskForH2AppraiseeSignOff_TaskId";
            this.onTaskChangedForH2AppraiseeSignOff.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForH2AppraiseeSignOff_Invoked);
            this.onTaskChangedForH2AppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind90)));
            this.onTaskChangedForH2AppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind91)));
            this.onTaskChangedForH2AppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind89)));
            // 
            // logToHistoryListActivity27
            // 
            this.logToHistoryListActivity27.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity27.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind92.Name = "VFS_AppraisalWorkflow";
            activitybind92.Path = "logToHistory1_HistoryDescription";
            activitybind93.Name = "VFS_AppraisalWorkflow";
            activitybind93.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity27.Name = "logToHistoryListActivity27";
            this.logToHistoryListActivity27.OtherData = "";
            this.logToHistoryListActivity27.UserId = -1;
            this.logToHistoryListActivity27.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind92)));
            this.logToHistoryListActivity27.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind93)));
            // 
            // createTaskForH2AppraiseeSignOff
            // 
            this.createTaskForH2AppraiseeSignOff.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2AppraiseeSignOff.CorrelationToken = correlationtoken6;
            this.createTaskForH2AppraiseeSignOff.ListItemId = -1;
            this.createTaskForH2AppraiseeSignOff.Name = "createTaskForH2AppraiseeSignOff";
            this.createTaskForH2AppraiseeSignOff.SpecialPermissions = null;
            activitybind94.Name = "VFS_AppraisalWorkflow";
            activitybind94.Path = "createTaskForH2AppraiseeSignOff_TaskId";
            activitybind95.Name = "VFS_AppraisalWorkflow";
            activitybind95.Path = "createTaskForH2AppraiseeSignOff_TaskProperties";
            this.createTaskForH2AppraiseeSignOff.MethodInvoking += new System.EventHandler(this.createTaskForH2AppraiseeSignOff_MethodInvoking);
            this.createTaskForH2AppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind95)));
            this.createTaskForH2AppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind94)));
            // 
            // ifElseH2RvrEvalDeactivation
            // 
            this.ifElseH2RvrEvalDeactivation.Activities.Add(this.ifH2RvrDeactivation);
            this.ifElseH2RvrEvalDeactivation.Activities.Add(this.ifElseBranchActivity15);
            this.ifElseH2RvrEvalDeactivation.Name = "ifElseH2RvrEvalDeactivation";
            // 
            // onTaskChangedH2ReviewerVerification
            // 
            activitybind96.Name = "VFS_AppraisalWorkflow";
            activitybind96.Path = "onTaskChangedH2ReviewerVerification_AfterProperties";
            activitybind97.Name = "VFS_AppraisalWorkflow";
            activitybind97.Path = "onTaskChangedH2ReviewerVerification_BeforeProperties";
            correlationtoken7.Name = "ctH2ReviewerVerification";
            correlationtoken7.OwnerActivityName = "H2ReviewerVerification";
            this.onTaskChangedH2ReviewerVerification.CorrelationToken = correlationtoken7;
            this.onTaskChangedH2ReviewerVerification.Executor = null;
            this.onTaskChangedH2ReviewerVerification.Name = "onTaskChangedH2ReviewerVerification";
            activitybind98.Name = "VFS_AppraisalWorkflow";
            activitybind98.Path = "createTaskForH2ReviewerVerification_TaskId";
            this.onTaskChangedH2ReviewerVerification.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedH2ReviewerVerification_Invoked);
            this.onTaskChangedH2ReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind97)));
            this.onTaskChangedH2ReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind98)));
            this.onTaskChangedH2ReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind96)));
            // 
            // logToHistoryListActivity25
            // 
            this.logToHistoryListActivity25.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity25.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind99.Name = "VFS_AppraisalWorkflow";
            activitybind99.Path = "logToHistory1_HistoryDescription";
            activitybind100.Name = "VFS_AppraisalWorkflow";
            activitybind100.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity25.Name = "logToHistoryListActivity25";
            this.logToHistoryListActivity25.OtherData = "";
            this.logToHistoryListActivity25.UserId = -1;
            this.logToHistoryListActivity25.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind99)));
            this.logToHistoryListActivity25.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind100)));
            // 
            // createTaskForH2ReviewerVerification
            // 
            this.createTaskForH2ReviewerVerification.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2ReviewerVerification.CorrelationToken = correlationtoken7;
            this.createTaskForH2ReviewerVerification.ListItemId = -1;
            this.createTaskForH2ReviewerVerification.Name = "createTaskForH2ReviewerVerification";
            this.createTaskForH2ReviewerVerification.SpecialPermissions = null;
            activitybind101.Name = "VFS_AppraisalWorkflow";
            activitybind101.Path = "createTaskForH2ReviewerVerification_TaskId";
            activitybind102.Name = "VFS_AppraisalWorkflow";
            activitybind102.Path = "createTaskForH2ReviewerVerification_TaskProperties";
            this.createTaskForH2ReviewerVerification.MethodInvoking += new System.EventHandler(this.createTaskForH2ReviewerVerification_MethodInvoking);
            this.createTaskForH2ReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind102)));
            this.createTaskForH2ReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind101)));
            // 
            // ifElseH2AppraiserEvalDeactivation
            // 
            this.ifElseH2AppraiserEvalDeactivation.Activities.Add(this.ifElseBranchActivity1);
            this.ifElseH2AppraiserEvalDeactivation.Activities.Add(this.ifElseBranchActivity13);
            this.ifElseH2AppraiserEvalDeactivation.Name = "ifElseH2AppraiserEvalDeactivation";
            // 
            // onTaskChangedForH2AppraiserEvaluation
            // 
            activitybind103.Name = "VFS_AppraisalWorkflow";
            activitybind103.Path = "onTaskChangedForH2AppraiserEvaluation_AfterProperties";
            activitybind104.Name = "VFS_AppraisalWorkflow";
            activitybind104.Path = "onTaskChangedForH2AppraiserEvaluation_BeforeProperties";
            correlationtoken8.Name = "ctH2AppraiserEvaluation";
            correlationtoken8.OwnerActivityName = "H2AppraiserEvaluation";
            this.onTaskChangedForH2AppraiserEvaluation.CorrelationToken = correlationtoken8;
            this.onTaskChangedForH2AppraiserEvaluation.Executor = null;
            this.onTaskChangedForH2AppraiserEvaluation.Name = "onTaskChangedForH2AppraiserEvaluation";
            activitybind105.Name = "VFS_AppraisalWorkflow";
            activitybind105.Path = "createTaskForH2AppraiserEvaluation_TaskId";
            this.onTaskChangedForH2AppraiserEvaluation.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForH2AppraiserEvaluation_Invoked);
            this.onTaskChangedForH2AppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind104)));
            this.onTaskChangedForH2AppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind105)));
            this.onTaskChangedForH2AppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind103)));
            // 
            // logToHistoryListActivity23
            // 
            this.logToHistoryListActivity23.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity23.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind106.Name = "VFS_AppraisalWorkflow";
            activitybind106.Path = "logToHistory1_HistoryDescription";
            activitybind107.Name = "VFS_AppraisalWorkflow";
            activitybind107.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity23.Name = "logToHistoryListActivity23";
            this.logToHistoryListActivity23.OtherData = "";
            this.logToHistoryListActivity23.UserId = -1;
            this.logToHistoryListActivity23.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind106)));
            this.logToHistoryListActivity23.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind107)));
            // 
            // createTaskForH2AppraiserEvaluation
            // 
            this.createTaskForH2AppraiserEvaluation.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2AppraiserEvaluation.CorrelationToken = correlationtoken8;
            this.createTaskForH2AppraiserEvaluation.ListItemId = -1;
            this.createTaskForH2AppraiserEvaluation.Name = "createTaskForH2AppraiserEvaluation";
            this.createTaskForH2AppraiserEvaluation.SpecialPermissions = null;
            activitybind108.Name = "VFS_AppraisalWorkflow";
            activitybind108.Path = "createTaskForH2AppraiserEvaluation_TaskId";
            activitybind109.Name = "VFS_AppraisalWorkflow";
            activitybind109.Path = "createTaskForH2AppraiserEvaluation_TaskProperties";
            this.createTaskForH2AppraiserEvaluation.MethodInvoking += new System.EventHandler(this.createTaskForH2AppraiserEvaluation_MethodInvoking);
            this.createTaskForH2AppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind109)));
            this.createTaskForH2AppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind108)));
            // 
            // ifElseH2SelfDeactivation
            // 
            this.ifElseH2SelfDeactivation.Activities.Add(this.ifH2SelfDeactivation);
            this.ifElseH2SelfDeactivation.Activities.Add(this.ifElseBranchActivity11);
            this.ifElseH2SelfDeactivation.Name = "ifElseH2SelfDeactivation";
            // 
            // onTaskChangedH2SelfEvaluation
            // 
            activitybind110.Name = "VFS_AppraisalWorkflow";
            activitybind110.Path = "onTaskChangedH2SelfEvaluation_AfterProperties";
            activitybind111.Name = "VFS_AppraisalWorkflow";
            activitybind111.Path = "onTaskChangedH2SelfEvaluation_BeforeProperties";
            correlationtoken9.Name = "ctH2SelfEvaluation";
            correlationtoken9.OwnerActivityName = "H2SelfEvaluation";
            this.onTaskChangedH2SelfEvaluation.CorrelationToken = correlationtoken9;
            this.onTaskChangedH2SelfEvaluation.Executor = null;
            this.onTaskChangedH2SelfEvaluation.Name = "onTaskChangedH2SelfEvaluation";
            activitybind112.Name = "VFS_AppraisalWorkflow";
            activitybind112.Path = "createTaskForH2SelfEvaluation_TaskId";
            this.onTaskChangedH2SelfEvaluation.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedH2SelfEvaluation_Invoked);
            this.onTaskChangedH2SelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind111)));
            this.onTaskChangedH2SelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind112)));
            this.onTaskChangedH2SelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind110)));
            // 
            // logToHistoryListActivity21
            // 
            this.logToHistoryListActivity21.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity21.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind113.Name = "VFS_AppraisalWorkflow";
            activitybind113.Path = "logToHistory1_HistoryDescription";
            activitybind114.Name = "VFS_AppraisalWorkflow";
            activitybind114.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity21.Name = "logToHistoryListActivity21";
            this.logToHistoryListActivity21.OtherData = "";
            this.logToHistoryListActivity21.UserId = -1;
            this.logToHistoryListActivity21.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind113)));
            this.logToHistoryListActivity21.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind114)));
            // 
            // createTaskForH2SelfEvaluation
            // 
            this.createTaskForH2SelfEvaluation.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2SelfEvaluation.CorrelationToken = correlationtoken9;
            this.createTaskForH2SelfEvaluation.ListItemId = -1;
            this.createTaskForH2SelfEvaluation.Name = "createTaskForH2SelfEvaluation";
            this.createTaskForH2SelfEvaluation.SpecialPermissions = null;
            activitybind115.Name = "VFS_AppraisalWorkflow";
            activitybind115.Path = "createTaskForH2SelfEvaluation_TaskId";
            activitybind116.Name = "VFS_AppraisalWorkflow";
            activitybind116.Path = "createTaskForH2SelfEvaluation_TaskProperties";
            this.createTaskForH2SelfEvaluation.MethodInvoking += new System.EventHandler(this.createTaskForH2SelfEvaluation_MethodInvoking);
            this.createTaskForH2SelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind116)));
            this.createTaskForH2SelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind115)));
            // 
            // ifElseH2AppraiserDeactivation
            // 
            this.ifElseH2AppraiserDeactivation.Activities.Add(this.ifH2AppraiserDeactivation);
            this.ifElseH2AppraiserDeactivation.Activities.Add(this.ifElseBranchActivity10);
            this.ifElseH2AppraiserDeactivation.Name = "ifElseH2AppraiserDeactivation";
            // 
            // onTaskChangedForAppraiserH2GoalVerification
            // 
            activitybind117.Name = "VFS_AppraisalWorkflow";
            activitybind117.Path = "onTaskChangedForAppraiserH2GoalVerification_AfterProperties";
            activitybind118.Name = "VFS_AppraisalWorkflow";
            activitybind118.Path = "onTaskChangedForAppraiserH2GoalVerification_BeforeProperties";
            correlationtoken10.Name = "ctCreateTaskForH2GoalVerification";
            correlationtoken10.OwnerActivityName = "AppraiserH2GoalVerification";
            this.onTaskChangedForAppraiserH2GoalVerification.CorrelationToken = correlationtoken10;
            this.onTaskChangedForAppraiserH2GoalVerification.Executor = null;
            this.onTaskChangedForAppraiserH2GoalVerification.Name = "onTaskChangedForAppraiserH2GoalVerification";
            activitybind119.Name = "VFS_AppraisalWorkflow";
            activitybind119.Path = "createTaskForH2InitialGoalSetting_TaskId";
            this.onTaskChangedForAppraiserH2GoalVerification.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForAppraiserH2GoalVerification_Invoked);
            this.onTaskChangedForAppraiserH2GoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind118)));
            this.onTaskChangedForAppraiserH2GoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind119)));
            this.onTaskChangedForAppraiserH2GoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind117)));
            // 
            // logToHistoryListActivity19
            // 
            this.logToHistoryListActivity19.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity19.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity19.HistoryDescription = "";
            this.logToHistoryListActivity19.HistoryOutcome = "";
            this.logToHistoryListActivity19.Name = "logToHistoryListActivity19";
            this.logToHistoryListActivity19.OtherData = "";
            this.logToHistoryListActivity19.UserId = -1;
            // 
            // createTaskForAppraiserH2GoalVerification
            // 
            this.createTaskForAppraiserH2GoalVerification.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForAppraiserH2GoalVerification.CorrelationToken = correlationtoken10;
            this.createTaskForAppraiserH2GoalVerification.ListItemId = -1;
            this.createTaskForAppraiserH2GoalVerification.Name = "createTaskForAppraiserH2GoalVerification";
            this.createTaskForAppraiserH2GoalVerification.SpecialPermissions = null;
            activitybind120.Name = "VFS_AppraisalWorkflow";
            activitybind120.Path = "createTaskForAppraiserH2GoalVerification_TaskId";
            activitybind121.Name = "VFS_AppraisalWorkflow";
            activitybind121.Path = "createTaskForAppraiserH2GoalVerification_TaskProperties";
            this.createTaskForAppraiserH2GoalVerification.MethodInvoking += new System.EventHandler(this.createTaskForAppraiserH2GoalVerification_MethodInvoking);
            this.createTaskForAppraiserH2GoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind121)));
            this.createTaskForAppraiserH2GoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind120)));
            // 
            // ifElseH2GoalSetting
            // 
            this.ifElseH2GoalSetting.Activities.Add(this.ifH2GoalsCompleted);
            this.ifElseH2GoalSetting.Activities.Add(this.ifElseBranchActivity9);
            this.ifElseH2GoalSetting.Name = "ifElseH2GoalSetting";
            // 
            // onTaskChangedH2GoalSetting
            // 
            activitybind122.Name = "VFS_AppraisalWorkflow";
            activitybind122.Path = "onTaskChangedH2GoalSetting_AfterProperties";
            activitybind123.Name = "VFS_AppraisalWorkflow";
            activitybind123.Path = "onTaskChangedH2GoalSetting_BeforeProperties";
            correlationtoken11.Name = "ctCreateTaskForH2InitialGoalSetting";
            correlationtoken11.OwnerActivityName = "H2InitialGoalSetting";
            this.onTaskChangedH2GoalSetting.CorrelationToken = correlationtoken11;
            this.onTaskChangedH2GoalSetting.Executor = null;
            this.onTaskChangedH2GoalSetting.Name = "onTaskChangedH2GoalSetting";
            activitybind124.Name = "VFS_AppraisalWorkflow";
            activitybind124.Path = "createTaskForH2InitialGoalSetting_TaskId";
            this.onTaskChangedH2GoalSetting.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedH2GoalSetting_Invoked);
            this.onTaskChangedH2GoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind124)));
            this.onTaskChangedH2GoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind122)));
            this.onTaskChangedH2GoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind123)));
            // 
            // logToHistoryListActivity17
            // 
            this.logToHistoryListActivity17.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity17.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logToHistoryListActivity17.HistoryDescription = "";
            this.logToHistoryListActivity17.HistoryOutcome = "";
            this.logToHistoryListActivity17.Name = "logToHistoryListActivity17";
            this.logToHistoryListActivity17.OtherData = "";
            this.logToHistoryListActivity17.UserId = -1;
            // 
            // createTaskForH2InitialGoalSetting
            // 
            this.createTaskForH2InitialGoalSetting.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForH2InitialGoalSetting.CorrelationToken = correlationtoken11;
            this.createTaskForH2InitialGoalSetting.ListItemId = -1;
            this.createTaskForH2InitialGoalSetting.Name = "createTaskForH2InitialGoalSetting";
            this.createTaskForH2InitialGoalSetting.SpecialPermissions = null;
            activitybind125.Name = "VFS_AppraisalWorkflow";
            activitybind125.Path = "createTaskForH2InitialGoalSetting_TaskId";
            activitybind126.Name = "VFS_AppraisalWorkflow";
            activitybind126.Path = "createTaskForH2InitialGoalSetting_TaskProperties";
            this.createTaskForH2InitialGoalSetting.MethodInvoking += new System.EventHandler(this.createTaskForH2InitialGoalSetting_MethodInvoking);
            this.createTaskForH2InitialGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind125)));
            this.createTaskForH2InitialGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind126)));
            // 
            // ifElseH1AppraiseeDeactivation
            // 
            this.ifElseH1AppraiseeDeactivation.Activities.Add(this.ifH1Deactivated);
            this.ifElseH1AppraiseeDeactivation.Activities.Add(this.ifH1GoalSettingComplete);
            this.ifElseH1AppraiseeDeactivation.Name = "ifElseH1AppraiseeDeactivation";
            // 
            // onTaskChangedGoalSetting
            // 
            activitybind127.Name = "VFS_AppraisalWorkflow";
            activitybind127.Path = "onTaskChangedGoalSetting_AfterProperties";
            activitybind128.Name = "VFS_AppraisalWorkflow";
            activitybind128.Path = "onTaskChangedGoalSetting_BeforeProperties";
            correlationtoken12.Name = "ctCreateTaskForInitialGoalSetting";
            correlationtoken12.OwnerActivityName = "AppraiseeGoalSetting";
            this.onTaskChangedGoalSetting.CorrelationToken = correlationtoken12;
            this.onTaskChangedGoalSetting.Executor = null;
            this.onTaskChangedGoalSetting.Name = "onTaskChangedGoalSetting";
            activitybind129.Name = "VFS_AppraisalWorkflow";
            activitybind129.Path = "createTaskForInitialGoalSetting_TaskId";
            this.onTaskChangedGoalSetting.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedGoalSetting_Invoked);
            this.onTaskChangedGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind128)));
            this.onTaskChangedGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind129)));
            this.onTaskChangedGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind127)));
            // 
            // logToHistoryListActivity15
            // 
            this.logToHistoryListActivity15.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity15.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind130.Name = "VFS_AppraisalWorkflow";
            activitybind130.Path = "logToHistory1_HistoryDescription";
            activitybind131.Name = "VFS_AppraisalWorkflow";
            activitybind131.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity15.Name = "logToHistoryListActivity15";
            this.logToHistoryListActivity15.OtherData = "";
            this.logToHistoryListActivity15.UserId = -1;
            this.logToHistoryListActivity15.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind130)));
            this.logToHistoryListActivity15.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind131)));
            // 
            // createTaskForInitialGoalSetting
            // 
            this.createTaskForInitialGoalSetting.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            this.createTaskForInitialGoalSetting.CorrelationToken = correlationtoken12;
            this.createTaskForInitialGoalSetting.ListItemId = -1;
            this.createTaskForInitialGoalSetting.Name = "createTaskForInitialGoalSetting";
            this.createTaskForInitialGoalSetting.SpecialPermissions = null;
            activitybind132.Name = "VFS_AppraisalWorkflow";
            activitybind132.Path = "createTaskForInitialGoalSetting_TaskId";
            activitybind133.Name = "VFS_AppraisalWorkflow";
            activitybind133.Path = "createTaskForInitialGoalSetting_TaskProperties";
            this.createTaskForInitialGoalSetting.MethodInvoking += new System.EventHandler(this.createTaskForInitialGoalSetting_MethodInvoking);
            this.createTaskForInitialGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind132)));
            this.createTaskForInitialGoalSetting.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind133)));
            // 
            // ifElseH1AppraiseeSingOffDeactivation
            // 
            this.ifElseH1AppraiseeSingOffDeactivation.Activities.Add(this.ifH1AppraiseeSignOffDeactivation);
            this.ifElseH1AppraiseeSingOffDeactivation.Activities.Add(this.ifElseBranchActivity7);
            this.ifElseH1AppraiseeSingOffDeactivation.Name = "ifElseH1AppraiseeSingOffDeactivation";
            // 
            // onTaskChangedForAppraiseeSignOff
            // 
            activitybind134.Name = "VFS_AppraisalWorkflow";
            activitybind134.Path = "onTaskChangedForAppraiseeSignOff_AfterProperties";
            activitybind135.Name = "VFS_AppraisalWorkflow";
            activitybind135.Path = "onTaskChangedForAppraiseeSignOff_BeforeProperties";
            correlationtoken13.Name = "ctAppraiseeSignOff";
            correlationtoken13.OwnerActivityName = "AppraiseeSignOffOrAppeal";
            this.onTaskChangedForAppraiseeSignOff.CorrelationToken = correlationtoken13;
            this.onTaskChangedForAppraiseeSignOff.Executor = null;
            this.onTaskChangedForAppraiseeSignOff.Name = "onTaskChangedForAppraiseeSignOff";
            activitybind136.Name = "VFS_AppraisalWorkflow";
            activitybind136.Path = "createTaskForAppraiseeSignOff_TaskId";
            this.onTaskChangedForAppraiseeSignOff.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForAppraiseeSignOff_Invoked);
            this.onTaskChangedForAppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind135)));
            this.onTaskChangedForAppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind136)));
            this.onTaskChangedForAppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind134)));
            // 
            // logToHistoryListActivity9
            // 
            this.logToHistoryListActivity9.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity9.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind137.Name = "VFS_AppraisalWorkflow";
            activitybind137.Path = "logToHistory1_HistoryDescription";
            activitybind138.Name = "VFS_AppraisalWorkflow";
            activitybind138.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity9.Name = "logToHistoryListActivity9";
            this.logToHistoryListActivity9.OtherData = "";
            this.logToHistoryListActivity9.UserId = -1;
            this.logToHistoryListActivity9.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind137)));
            this.logToHistoryListActivity9.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind138)));
            // 
            // createTaskForAppraiseeSignOff
            // 
            this.createTaskForAppraiseeSignOff.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken14.Name = "ctAppraiseeSignOff";
            correlationtoken14.OwnerActivityName = "AppraiseeSignOffOrAppeal";
            this.createTaskForAppraiseeSignOff.CorrelationToken = correlationtoken14;
            this.createTaskForAppraiseeSignOff.ListItemId = -1;
            this.createTaskForAppraiseeSignOff.Name = "createTaskForAppraiseeSignOff";
            this.createTaskForAppraiseeSignOff.SpecialPermissions = null;
            activitybind139.Name = "VFS_AppraisalWorkflow";
            activitybind139.Path = "createTaskForAppraiseeSignOff_TaskId";
            activitybind140.Name = "VFS_AppraisalWorkflow";
            activitybind140.Path = "createTaskForAppraiseeSignOff_TaskProperties";
            this.createTaskForAppraiseeSignOff.MethodInvoking += new System.EventHandler(this.createTaskForAppraiseeSignOff_MethodInvoking);
            this.createTaskForAppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind140)));
            this.createTaskForAppraiseeSignOff.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind139)));
            // 
            // ifElseH1HRDeactivation
            // 
            this.ifElseH1HRDeactivation.Activities.Add(this.ifH1HRDeactivation);
            this.ifElseH1HRDeactivation.Activities.Add(this.ifElseBranchActivity8);
            this.ifElseH1HRDeactivation.Name = "ifElseH1HRDeactivation";
            // 
            // onTaskChangedForCountryHR
            // 
            activitybind141.Name = "VFS_AppraisalWorkflow";
            activitybind141.Path = "onTaskChangedForCountryHR_AfterProperties";
            activitybind142.Name = "VFS_AppraisalWorkflow";
            activitybind142.Path = "onTaskChangedForCountryHR_BeforeProperties";
            correlationtoken15.Name = "ctCountryHR";
            correlationtoken15.OwnerActivityName = "HR";
            this.onTaskChangedForCountryHR.CorrelationToken = correlationtoken15;
            this.onTaskChangedForCountryHR.Executor = null;
            this.onTaskChangedForCountryHR.Name = "onTaskChangedForCountryHR";
            activitybind143.Name = "VFS_AppraisalWorkflow";
            activitybind143.Path = "createTaskForCountryHR_TaskId";
            this.onTaskChangedForCountryHR.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForCountryHR_Invoked);
            this.onTaskChangedForCountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind142)));
            this.onTaskChangedForCountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind143)));
            this.onTaskChangedForCountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind141)));
            // 
            // logToHistoryListActivity12
            // 
            this.logToHistoryListActivity12.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity12.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind144.Name = "VFS_AppraisalWorkflow";
            activitybind144.Path = "logToHistory1_HistoryDescription";
            activitybind145.Name = "VFS_AppraisalWorkflow";
            activitybind145.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity12.Name = "logToHistoryListActivity12";
            this.logToHistoryListActivity12.OtherData = "";
            this.logToHistoryListActivity12.UserId = -1;
            this.logToHistoryListActivity12.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind144)));
            this.logToHistoryListActivity12.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind145)));
            // 
            // createTaskForCountryHR
            // 
            this.createTaskForCountryHR.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken16.Name = "ctCountryHR";
            correlationtoken16.OwnerActivityName = "HR";
            this.createTaskForCountryHR.CorrelationToken = correlationtoken16;
            this.createTaskForCountryHR.ListItemId = -1;
            this.createTaskForCountryHR.Name = "createTaskForCountryHR";
            this.createTaskForCountryHR.SpecialPermissions = null;
            activitybind146.Name = "VFS_AppraisalWorkflow";
            activitybind146.Path = "createTaskForCountryHR_TaskId";
            activitybind147.Name = "VFS_AppraisalWorkflow";
            activitybind147.Path = "createTaskForCountryHR_TaskProperties";
            this.createTaskForCountryHR.MethodInvoking += new System.EventHandler(this.createTaskForCountryHR_MethodInvoking);
            this.createTaskForCountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind147)));
            this.createTaskForCountryHR.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind146)));
            // 
            // ifElseH1RvrEvalDeactiovation
            // 
            this.ifElseH1RvrEvalDeactiovation.Activities.Add(this.ifRvrEvalDeactivation);
            this.ifElseH1RvrEvalDeactiovation.Activities.Add(this.ifElseBranchActivity6);
            this.ifElseH1RvrEvalDeactiovation.Name = "ifElseH1RvrEvalDeactiovation";
            // 
            // onTaskChangedReviewerVerification
            // 
            activitybind148.Name = "VFS_AppraisalWorkflow";
            activitybind148.Path = "onTaskChangedReviewerVerification_AfterProperties";
            activitybind149.Name = "VFS_AppraisalWorkflow";
            activitybind149.Path = "onTaskChangedReviewerVerification_BeforeProperties";
            correlationtoken17.Name = "ctReviewerVerification";
            correlationtoken17.OwnerActivityName = "ReviewerVerification";
            this.onTaskChangedReviewerVerification.CorrelationToken = correlationtoken17;
            this.onTaskChangedReviewerVerification.Executor = null;
            this.onTaskChangedReviewerVerification.Name = "onTaskChangedReviewerVerification";
            activitybind150.Name = "VFS_AppraisalWorkflow";
            activitybind150.Path = "createTaskForReviewerVerification_TaskId";
            this.onTaskChangedReviewerVerification.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedReviewerVerification_Invoked);
            this.onTaskChangedReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind149)));
            this.onTaskChangedReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind150)));
            this.onTaskChangedReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind148)));
            // 
            // logToHistoryListActivity7
            // 
            this.logToHistoryListActivity7.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity7.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind151.Name = "VFS_AppraisalWorkflow";
            activitybind151.Path = "logToHistory1_HistoryDescription";
            activitybind152.Name = "VFS_AppraisalWorkflow";
            activitybind152.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity7.Name = "logToHistoryListActivity7";
            this.logToHistoryListActivity7.OtherData = "";
            this.logToHistoryListActivity7.UserId = -1;
            this.logToHistoryListActivity7.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind151)));
            this.logToHistoryListActivity7.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind152)));
            // 
            // createTaskForReviewerVerification
            // 
            this.createTaskForReviewerVerification.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken18.Name = "ctReviewerVerification";
            correlationtoken18.OwnerActivityName = "ReviewerVerification";
            this.createTaskForReviewerVerification.CorrelationToken = correlationtoken18;
            this.createTaskForReviewerVerification.ListItemId = -1;
            this.createTaskForReviewerVerification.Name = "createTaskForReviewerVerification";
            this.createTaskForReviewerVerification.SpecialPermissions = null;
            activitybind153.Name = "VFS_AppraisalWorkflow";
            activitybind153.Path = "createTaskForReviewerVerification_TaskId";
            activitybind154.Name = "VFS_AppraisalWorkflow";
            activitybind154.Path = "createTaskForReviewerVerification_TaskProperties";
            this.createTaskForReviewerVerification.MethodInvoking += new System.EventHandler(this.createTaskForReviewerVerification_MethodInvoking);
            this.createTaskForReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind154)));
            this.createTaskForReviewerVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind153)));
            // 
            // ifH1AppraiserEvalDeactivation
            // 
            this.ifH1AppraiserEvalDeactivation.Activities.Add(this.ifH1AppraiserEvalDeactivated);
            this.ifH1AppraiserEvalDeactivation.Activities.Add(this.ifElseBranchActivity5);
            this.ifH1AppraiserEvalDeactivation.Name = "ifH1AppraiserEvalDeactivation";
            // 
            // onTaskChangedForAppraiserEvaluation
            // 
            activitybind155.Name = "VFS_AppraisalWorkflow";
            activitybind155.Path = "onTaskChangedForAppraiserEvaluation_AfterProperties";
            activitybind156.Name = "VFS_AppraisalWorkflow";
            activitybind156.Path = "onTaskChangedForAppraiserEvaluation_BeforeProperties";
            correlationtoken19.Name = "ctAppraiserEvaluation";
            correlationtoken19.OwnerActivityName = "AppraiserEvaluation";
            this.onTaskChangedForAppraiserEvaluation.CorrelationToken = correlationtoken19;
            this.onTaskChangedForAppraiserEvaluation.Executor = null;
            this.onTaskChangedForAppraiserEvaluation.Name = "onTaskChangedForAppraiserEvaluation";
            activitybind157.Name = "VFS_AppraisalWorkflow";
            activitybind157.Path = "createTaskForAppraiserEvaluation_TaskId";
            this.onTaskChangedForAppraiserEvaluation.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForAppraiserEvaluation_Invoked);
            this.onTaskChangedForAppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind156)));
            this.onTaskChangedForAppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind157)));
            this.onTaskChangedForAppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind155)));
            // 
            // logToHistoryListActivity5
            // 
            this.logToHistoryListActivity5.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity5.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind158.Name = "VFS_AppraisalWorkflow";
            activitybind158.Path = "logToHistory1_HistoryDescription";
            activitybind159.Name = "VFS_AppraisalWorkflow";
            activitybind159.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity5.Name = "logToHistoryListActivity5";
            this.logToHistoryListActivity5.OtherData = "";
            this.logToHistoryListActivity5.UserId = -1;
            this.logToHistoryListActivity5.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind158)));
            this.logToHistoryListActivity5.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind159)));
            // 
            // createTaskForAppraiserEvaluation
            // 
            this.createTaskForAppraiserEvaluation.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken20.Name = "ctAppraiserEvaluation";
            correlationtoken20.OwnerActivityName = "AppraiserEvaluation";
            this.createTaskForAppraiserEvaluation.CorrelationToken = correlationtoken20;
            this.createTaskForAppraiserEvaluation.ListItemId = -1;
            this.createTaskForAppraiserEvaluation.Name = "createTaskForAppraiserEvaluation";
            this.createTaskForAppraiserEvaluation.SpecialPermissions = null;
            activitybind160.Name = "VFS_AppraisalWorkflow";
            activitybind160.Path = "createTaskForAppraiserEvaluation_TaskId";
            activitybind161.Name = "VFS_AppraisalWorkflow";
            activitybind161.Path = "createTaskForAppraiserEvaluation_TaskProperties";
            this.createTaskForAppraiserEvaluation.MethodInvoking += new System.EventHandler(this.createTaskForAppraiserEvaluation_MethodInvoking);
            this.createTaskForAppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind161)));
            this.createTaskForAppraiserEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind160)));
            // 
            // ifElseH1SelfDeactivation
            // 
            this.ifElseH1SelfDeactivation.Activities.Add(this.ifH1SelfDeactivation);
            this.ifElseH1SelfDeactivation.Activities.Add(this.ifElseBranchActivity2);
            this.ifElseH1SelfDeactivation.Name = "ifElseH1SelfDeactivation";
            // 
            // onTaskChangedSelfEvaluation
            // 
            activitybind162.Name = "VFS_AppraisalWorkflow";
            activitybind162.Path = "onTaskChangedSelfEvaluation_AfterProperties";
            activitybind163.Name = "VFS_AppraisalWorkflow";
            activitybind163.Path = "onTaskChangedSelfEvaluation_BeforeProperties";
            correlationtoken21.Name = "ctSelfEvaluation";
            correlationtoken21.OwnerActivityName = "SelfEvaluation";
            this.onTaskChangedSelfEvaluation.CorrelationToken = correlationtoken21;
            this.onTaskChangedSelfEvaluation.Executor = null;
            this.onTaskChangedSelfEvaluation.Name = "onTaskChangedSelfEvaluation";
            activitybind164.Name = "VFS_AppraisalWorkflow";
            activitybind164.Path = "createTaskForSelfEvaluation_TaskId";
            this.onTaskChangedSelfEvaluation.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedSelfEvaluation_Invoked);
            this.onTaskChangedSelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind163)));
            this.onTaskChangedSelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind164)));
            this.onTaskChangedSelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind162)));
            // 
            // logToHistoryListActivity3
            // 
            this.logToHistoryListActivity3.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistoryListActivity3.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind165.Name = "VFS_AppraisalWorkflow";
            activitybind165.Path = "logToHistory1_HistoryDescription";
            activitybind166.Name = "VFS_AppraisalWorkflow";
            activitybind166.Path = "logToHistory1_HistoryOutcome";
            this.logToHistoryListActivity3.Name = "logToHistoryListActivity3";
            this.logToHistoryListActivity3.OtherData = "";
            this.logToHistoryListActivity3.UserId = -1;
            this.logToHistoryListActivity3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind165)));
            this.logToHistoryListActivity3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind166)));
            // 
            // createTaskForSelfEvaluation
            // 
            this.createTaskForSelfEvaluation.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken22.Name = "ctSelfEvaluation";
            correlationtoken22.OwnerActivityName = "SelfEvaluation";
            this.createTaskForSelfEvaluation.CorrelationToken = correlationtoken22;
            this.createTaskForSelfEvaluation.ListItemId = -1;
            this.createTaskForSelfEvaluation.Name = "createTaskForSelfEvaluation";
            this.createTaskForSelfEvaluation.SpecialPermissions = null;
            activitybind167.Name = "VFS_AppraisalWorkflow";
            activitybind167.Path = "createTaskForSelfEvaluation_TaskId";
            activitybind168.Name = "VFS_AppraisalWorkflow";
            activitybind168.Path = "createTaskForSelfEvaluation_TaskProperties";
            this.createTaskForSelfEvaluation.MethodInvoking += new System.EventHandler(this.createTaskForSelfEvaluation_MethodInvoking);
            this.createTaskForSelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind168)));
            this.createTaskForSelfEvaluation.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind167)));
            // 
            // ifElseH1AppraiserDeactivation
            // 
            this.ifElseH1AppraiserDeactivation.Activities.Add(this.ifH1AppraiserDeactivation);
            this.ifElseH1AppraiserDeactivation.Activities.Add(this.ifElseBranchActivity3);
            this.ifElseH1AppraiserDeactivation.Name = "ifElseH1AppraiserDeactivation";
            // 
            // onTaskChangedForAppraiserGoalVerification
            // 
            activitybind169.Name = "VFS_AppraisalWorkflow";
            activitybind169.Path = "onTaskChangedForAppraiserGoalVerification_AfterProperties";
            activitybind170.Name = "VFS_AppraisalWorkflow";
            activitybind170.Path = "onTaskChangedForAppraiserGoalVerification_AfterProperties";
            correlationtoken23.Name = "ctAppraiserGoalVerification";
            correlationtoken23.OwnerActivityName = "AppraiserGoalVerification";
            this.onTaskChangedForAppraiserGoalVerification.CorrelationToken = correlationtoken23;
            this.onTaskChangedForAppraiserGoalVerification.Executor = null;
            this.onTaskChangedForAppraiserGoalVerification.Name = "onTaskChangedForAppraiserGoalVerification";
            activitybind171.Name = "VFS_AppraisalWorkflow";
            activitybind171.Path = "createTaskForAppraiserGoalVerification_TaskId";
            this.onTaskChangedForAppraiserGoalVerification.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChangedForAppraiserGoalVerification_Invoked);
            this.onTaskChangedForAppraiserGoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.BeforePropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind170)));
            this.onTaskChangedForAppraiserGoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind171)));
            this.onTaskChangedForAppraiserGoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind169)));
            // 
            // logToHistory1
            // 
            this.logToHistory1.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logToHistory1.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind172.Name = "VFS_AppraisalWorkflow";
            activitybind172.Path = "logToHistory1_HistoryDescription";
            activitybind173.Name = "VFS_AppraisalWorkflow";
            activitybind173.Path = "logToHistory1_HistoryOutcome";
            this.logToHistory1.Name = "logToHistory1";
            this.logToHistory1.OtherData = "";
            this.logToHistory1.UserId = -1;
            this.logToHistory1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind172)));
            this.logToHistory1.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind173)));
            // 
            // createTaskForAppraiserGoalVerification
            // 
            this.createTaskForAppraiserGoalVerification.ContentTypeId = "0x01080100f546143b2f5f42c69cd68f01ac3a4f31";
            correlationtoken24.Name = "ctAppraiserGoalVerification";
            correlationtoken24.OwnerActivityName = "AppraiserGoalVerification";
            this.createTaskForAppraiserGoalVerification.CorrelationToken = correlationtoken24;
            this.createTaskForAppraiserGoalVerification.ListItemId = -1;
            this.createTaskForAppraiserGoalVerification.Name = "createTaskForAppraiserGoalVerification";
            this.createTaskForAppraiserGoalVerification.SpecialPermissions = null;
            activitybind174.Name = "VFS_AppraisalWorkflow";
            activitybind174.Path = "createTaskForAppraiserGoalVerification_TaskId";
            activitybind175.Name = "VFS_AppraisalWorkflow";
            activitybind175.Path = "createTaskForAppraiserGoalVerification_TaskProperties";
            this.createTaskForAppraiserGoalVerification.MethodInvoking += new System.EventHandler(this.createTaskForAppraiserGoalVerification_MethodInvoking);
            this.createTaskForAppraiserGoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind175)));
            this.createTaskForAppraiserGoalVerification.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind174)));
            // 
            // ifElseH2Started
            // 
            this.ifElseH2Started.Activities.Add(this.ifH2Started);
            this.ifElseH2Started.Activities.Add(this.ifH2NotStarted);
            this.ifElseH2Started.Name = "ifElseH2Started";
            // 
            // caCheckH1orH2
            // 
            this.caCheckH1orH2.Name = "caCheckH1orH2";
            this.caCheckH1orH2.ExecuteCode += new System.EventHandler(this.CheckH1orH2);
            // 
            // onWorkflowActivated1
            // 
            correlationtoken25.Name = "workflowToken";
            correlationtoken25.OwnerActivityName = "VFS_AppraisalWorkflow";
            this.onWorkflowActivated1.CorrelationToken = correlationtoken25;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind176.Name = "VFS_AppraisalWorkflow";
            activitybind176.Path = "workflowProperties";
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind176)));
            // 
            // edH1Deactivation
            // 
            this.edH1Deactivation.Activities.Add(this.onTaskChangedH1Deactivation);
            this.edH1Deactivation.Activities.Add(this.logToHistoryListActivity42);
            this.edH1Deactivation.Activities.Add(this.setStateToH2Start);
            this.edH1Deactivation.Name = "edH1Deactivation";
            // 
            // stateIniH1Deactivation
            // 
            this.stateIniH1Deactivation.Activities.Add(this.createTaskForDeactivation);
            this.stateIniH1Deactivation.Activities.Add(this.logToHistoryListActivity35);
            this.stateIniH1Deactivation.Name = "stateIniH1Deactivation";
            // 
            // edH2Empty
            // 
            this.edH2Empty.Activities.Add(this.onH2EmptyTaskChanged);
            this.edH2Empty.Activities.Add(this.ifElseH2EmptyDeactivation);
            this.edH2Empty.Name = "edH2Empty";
            // 
            // stateIniH2Empty
            // 
            this.stateIniH2Empty.Activities.Add(this.createTaskForH2Empty);
            this.stateIniH2Empty.Activities.Add(this.logToHistoryListActivity33);
            this.stateIniH2Empty.Name = "stateIniH2Empty";
            // 
            // edH1Empty
            // 
            this.edH1Empty.Activities.Add(this.onH1EmptyTaskChanged);
            this.edH1Empty.Activities.Add(this.ifElseH1Empty);
            this.edH1Empty.Name = "edH1Empty";
            // 
            // stateIniH1Empty
            // 
            this.stateIniH1Empty.Activities.Add(this.createTaskForH1Empty);
            this.stateIniH1Empty.Activities.Add(this.logToHistoryListActivity36);
            this.stateIniH1Empty.Name = "stateIniH1Empty";
            // 
            // edH2HR
            // 
            this.edH2HR.Activities.Add(this.onTaskChangedForH2CountryHR);
            this.edH2HR.Activities.Add(this.ifElseH2HRDeactivation);
            this.edH2HR.Name = "edH2HR";
            // 
            // stateIniH2HR
            // 
            this.stateIniH2HR.Activities.Add(this.createTaskForH2CountryHR);
            this.stateIniH2HR.Activities.Add(this.logToHistoryListActivity30);
            this.stateIniH2HR.Name = "stateIniH2HR";
            // 
            // edH2AppraiseeSignOff
            // 
            this.edH2AppraiseeSignOff.Activities.Add(this.onTaskChangedForH2AppraiseeSignOff);
            this.edH2AppraiseeSignOff.Activities.Add(this.ifElseH2AppraiseeSignOff);
            this.edH2AppraiseeSignOff.Name = "edH2AppraiseeSignOff";
            // 
            // stateIniH2AppraiseeSignOff
            // 
            this.stateIniH2AppraiseeSignOff.Activities.Add(this.createTaskForH2AppraiseeSignOff);
            this.stateIniH2AppraiseeSignOff.Activities.Add(this.logToHistoryListActivity27);
            this.stateIniH2AppraiseeSignOff.Name = "stateIniH2AppraiseeSignOff";
            // 
            // edH2ReviewerVerification
            // 
            this.edH2ReviewerVerification.Activities.Add(this.onTaskChangedH2ReviewerVerification);
            this.edH2ReviewerVerification.Activities.Add(this.ifElseH2RvrEvalDeactivation);
            this.edH2ReviewerVerification.Name = "edH2ReviewerVerification";
            // 
            // stateIniH2ReviewerVerification
            // 
            this.stateIniH2ReviewerVerification.Activities.Add(this.createTaskForH2ReviewerVerification);
            this.stateIniH2ReviewerVerification.Activities.Add(this.logToHistoryListActivity25);
            this.stateIniH2ReviewerVerification.Name = "stateIniH2ReviewerVerification";
            // 
            // edH2AppraiserVerification
            // 
            this.edH2AppraiserVerification.Activities.Add(this.onTaskChangedForH2AppraiserEvaluation);
            this.edH2AppraiserVerification.Activities.Add(this.ifElseH2AppraiserEvalDeactivation);
            this.edH2AppraiserVerification.Name = "edH2AppraiserVerification";
            // 
            // stateIniH2AppraiserVerification
            // 
            this.stateIniH2AppraiserVerification.Activities.Add(this.createTaskForH2AppraiserEvaluation);
            this.stateIniH2AppraiserVerification.Activities.Add(this.logToHistoryListActivity23);
            this.stateIniH2AppraiserVerification.Name = "stateIniH2AppraiserVerification";
            // 
            // edH2SelfEvaluation
            // 
            this.edH2SelfEvaluation.Activities.Add(this.onTaskChangedH2SelfEvaluation);
            this.edH2SelfEvaluation.Activities.Add(this.ifElseH2SelfDeactivation);
            this.edH2SelfEvaluation.Name = "edH2SelfEvaluation";
            // 
            // stateIniH2SelfEvaluation
            // 
            this.stateIniH2SelfEvaluation.Activities.Add(this.createTaskForH2SelfEvaluation);
            this.stateIniH2SelfEvaluation.Activities.Add(this.logToHistoryListActivity21);
            this.stateIniH2SelfEvaluation.Name = "stateIniH2SelfEvaluation";
            // 
            // edAppraiserH2Verification
            // 
            this.edAppraiserH2Verification.Activities.Add(this.onTaskChangedForAppraiserH2GoalVerification);
            this.edAppraiserH2Verification.Activities.Add(this.ifElseH2AppraiserDeactivation);
            this.edAppraiserH2Verification.Name = "edAppraiserH2Verification";
            // 
            // H2AppraiserVerificationStateInitiation
            // 
            this.H2AppraiserVerificationStateInitiation.Activities.Add(this.createTaskForAppraiserH2GoalVerification);
            this.H2AppraiserVerificationStateInitiation.Activities.Add(this.logToHistoryListActivity19);
            this.H2AppraiserVerificationStateInitiation.Name = "H2AppraiserVerificationStateInitiation";
            // 
            // edAppraiseeH2InitialGoalSetting
            // 
            this.edAppraiseeH2InitialGoalSetting.Activities.Add(this.onTaskChangedH2GoalSetting);
            this.edAppraiseeH2InitialGoalSetting.Activities.Add(this.ifElseH2GoalSetting);
            this.edAppraiseeH2InitialGoalSetting.Name = "edAppraiseeH2InitialGoalSetting";
            // 
            // AppraiseeH2InitialGoalSetting
            // 
            this.AppraiseeH2InitialGoalSetting.Activities.Add(this.createTaskForH2InitialGoalSetting);
            this.AppraiseeH2InitialGoalSetting.Activities.Add(this.logToHistoryListActivity17);
            this.AppraiseeH2InitialGoalSetting.Name = "AppraiseeH2InitialGoalSetting";
            // 
            // edAppraiseeGoalSetting
            // 
            this.edAppraiseeGoalSetting.Activities.Add(this.onTaskChangedGoalSetting);
            this.edAppraiseeGoalSetting.Activities.Add(this.ifElseH1AppraiseeDeactivation);
            this.edAppraiseeGoalSetting.Name = "edAppraiseeGoalSetting";
            // 
            // AppraiseeInitialGoalSetting
            // 
            this.AppraiseeInitialGoalSetting.Activities.Add(this.createTaskForInitialGoalSetting);
            this.AppraiseeInitialGoalSetting.Activities.Add(this.logToHistoryListActivity15);
            this.AppraiseeInitialGoalSetting.Name = "AppraiseeInitialGoalSetting";
            // 
            // edAppraiseeSignOff
            // 
            this.edAppraiseeSignOff.Activities.Add(this.onTaskChangedForAppraiseeSignOff);
            this.edAppraiseeSignOff.Activities.Add(this.ifElseH1AppraiseeSingOffDeactivation);
            this.edAppraiseeSignOff.Name = "edAppraiseeSignOff";
            // 
            // stateIniAppraiseeSignOff
            // 
            this.stateIniAppraiseeSignOff.Activities.Add(this.createTaskForAppraiseeSignOff);
            this.stateIniAppraiseeSignOff.Activities.Add(this.logToHistoryListActivity9);
            this.stateIniAppraiseeSignOff.Name = "stateIniAppraiseeSignOff";
            // 
            // edHR
            // 
            this.edHR.Activities.Add(this.onTaskChangedForCountryHR);
            this.edHR.Activities.Add(this.ifElseH1HRDeactivation);
            this.edHR.Name = "edHR";
            // 
            // stateIniHR
            // 
            this.stateIniHR.Activities.Add(this.createTaskForCountryHR);
            this.stateIniHR.Activities.Add(this.logToHistoryListActivity12);
            this.stateIniHR.Name = "stateIniHR";
            // 
            // edReviewerVerification
            // 
            this.edReviewerVerification.Activities.Add(this.onTaskChangedReviewerVerification);
            this.edReviewerVerification.Activities.Add(this.ifElseH1RvrEvalDeactiovation);
            this.edReviewerVerification.Name = "edReviewerVerification";
            // 
            // stateIniReviewerVerification
            // 
            this.stateIniReviewerVerification.Activities.Add(this.createTaskForReviewerVerification);
            this.stateIniReviewerVerification.Activities.Add(this.logToHistoryListActivity7);
            this.stateIniReviewerVerification.Name = "stateIniReviewerVerification";
            // 
            // edAppraiserEvaluation
            // 
            this.edAppraiserEvaluation.Activities.Add(this.onTaskChangedForAppraiserEvaluation);
            this.edAppraiserEvaluation.Activities.Add(this.ifH1AppraiserEvalDeactivation);
            this.edAppraiserEvaluation.Name = "edAppraiserEvaluation";
            // 
            // stateIniAppraiserEvaluation
            // 
            this.stateIniAppraiserEvaluation.Activities.Add(this.createTaskForAppraiserEvaluation);
            this.stateIniAppraiserEvaluation.Activities.Add(this.logToHistoryListActivity5);
            this.stateIniAppraiserEvaluation.Name = "stateIniAppraiserEvaluation";
            // 
            // edSelfEvaluation
            // 
            this.edSelfEvaluation.Activities.Add(this.onTaskChangedSelfEvaluation);
            this.edSelfEvaluation.Activities.Add(this.ifElseH1SelfDeactivation);
            this.edSelfEvaluation.Name = "edSelfEvaluation";
            // 
            // stateIniSelfEvaluation
            // 
            this.stateIniSelfEvaluation.Activities.Add(this.createTaskForSelfEvaluation);
            this.stateIniSelfEvaluation.Activities.Add(this.logToHistoryListActivity3);
            this.stateIniSelfEvaluation.Name = "stateIniSelfEvaluation";
            // 
            // edAppraiserVerification
            // 
            this.edAppraiserVerification.Activities.Add(this.onTaskChangedForAppraiserGoalVerification);
            this.edAppraiserVerification.Activities.Add(this.ifElseH1AppraiserDeactivation);
            this.edAppraiserVerification.Name = "edAppraiserVerification";
            // 
            // AppraiserVerificationStateInitiation
            // 
            this.AppraiserVerificationStateInitiation.Activities.Add(this.createTaskForAppraiserGoalVerification);
            this.AppraiserVerificationStateInitiation.Activities.Add(this.logToHistory1);
            this.AppraiserVerificationStateInitiation.Name = "AppraiserVerificationStateInitiation";
            // 
            // edInitiation
            // 
            this.edInitiation.Activities.Add(this.onWorkflowActivated1);
            this.edInitiation.Activities.Add(this.caCheckH1orH2);
            this.edInitiation.Activities.Add(this.ifElseH2Started);
            this.edInitiation.Name = "edInitiation";
            // 
            // H1Deactivation
            // 
            this.H1Deactivation.Activities.Add(this.stateIniH1Deactivation);
            this.H1Deactivation.Activities.Add(this.edH1Deactivation);
            this.H1Deactivation.Name = "H1Deactivation";
            // 
            // H2EmptyState
            // 
            this.H2EmptyState.Activities.Add(this.stateIniH2Empty);
            this.H2EmptyState.Activities.Add(this.edH2Empty);
            this.H2EmptyState.Name = "H2EmptyState";
            // 
            // H1EmptyState
            // 
            this.H1EmptyState.Activities.Add(this.stateIniH1Empty);
            this.H1EmptyState.Activities.Add(this.edH1Empty);
            this.H1EmptyState.Name = "H1EmptyState";
            // 
            // H2HR
            // 
            this.H2HR.Activities.Add(this.stateIniH2HR);
            this.H2HR.Activities.Add(this.edH2HR);
            this.H2HR.Name = "H2HR";
            // 
            // H2AppraiseeSignOffOrAppeal
            // 
            this.H2AppraiseeSignOffOrAppeal.Activities.Add(this.stateIniH2AppraiseeSignOff);
            this.H2AppraiseeSignOffOrAppeal.Activities.Add(this.edH2AppraiseeSignOff);
            this.H2AppraiseeSignOffOrAppeal.Name = "H2AppraiseeSignOffOrAppeal";
            // 
            // H2ReviewerVerification
            // 
            this.H2ReviewerVerification.Activities.Add(this.stateIniH2ReviewerVerification);
            this.H2ReviewerVerification.Activities.Add(this.edH2ReviewerVerification);
            this.H2ReviewerVerification.Name = "H2ReviewerVerification";
            // 
            // H2AppraiserEvaluation
            // 
            this.H2AppraiserEvaluation.Activities.Add(this.stateIniH2AppraiserVerification);
            this.H2AppraiserEvaluation.Activities.Add(this.edH2AppraiserVerification);
            this.H2AppraiserEvaluation.Name = "H2AppraiserEvaluation";
            // 
            // H2SelfEvaluation
            // 
            this.H2SelfEvaluation.Activities.Add(this.stateIniH2SelfEvaluation);
            this.H2SelfEvaluation.Activities.Add(this.edH2SelfEvaluation);
            this.H2SelfEvaluation.Name = "H2SelfEvaluation";
            // 
            // AppraiserH2GoalVerification
            // 
            this.AppraiserH2GoalVerification.Activities.Add(this.H2AppraiserVerificationStateInitiation);
            this.AppraiserH2GoalVerification.Activities.Add(this.edAppraiserH2Verification);
            this.AppraiserH2GoalVerification.Name = "AppraiserH2GoalVerification";
            // 
            // H2InitialGoalSetting
            // 
            this.H2InitialGoalSetting.Activities.Add(this.AppraiseeH2InitialGoalSetting);
            this.H2InitialGoalSetting.Activities.Add(this.edAppraiseeH2InitialGoalSetting);
            this.H2InitialGoalSetting.Name = "H2InitialGoalSetting";
            // 
            // AppraiseeGoalSetting
            // 
            this.AppraiseeGoalSetting.Activities.Add(this.AppraiseeInitialGoalSetting);
            this.AppraiseeGoalSetting.Activities.Add(this.edAppraiseeGoalSetting);
            this.AppraiseeGoalSetting.Name = "AppraiseeGoalSetting";
            // 
            // AppraiseeSignOffOrAppeal
            // 
            this.AppraiseeSignOffOrAppeal.Activities.Add(this.stateIniAppraiseeSignOff);
            this.AppraiseeSignOffOrAppeal.Activities.Add(this.edAppraiseeSignOff);
            this.AppraiseeSignOffOrAppeal.Name = "AppraiseeSignOffOrAppeal";
            // 
            // ClosedState
            // 
            this.ClosedState.Name = "ClosedState";
            // 
            // HR
            // 
            this.HR.Activities.Add(this.stateIniHR);
            this.HR.Activities.Add(this.edHR);
            this.HR.Name = "HR";
            // 
            // ReviewerVerification
            // 
            this.ReviewerVerification.Activities.Add(this.stateIniReviewerVerification);
            this.ReviewerVerification.Activities.Add(this.edReviewerVerification);
            this.ReviewerVerification.Name = "ReviewerVerification";
            // 
            // AppraiserEvaluation
            // 
            this.AppraiserEvaluation.Activities.Add(this.stateIniAppraiserEvaluation);
            this.AppraiserEvaluation.Activities.Add(this.edAppraiserEvaluation);
            this.AppraiserEvaluation.Name = "AppraiserEvaluation";
            // 
            // SelfEvaluation
            // 
            this.SelfEvaluation.Activities.Add(this.stateIniSelfEvaluation);
            this.SelfEvaluation.Activities.Add(this.edSelfEvaluation);
            this.SelfEvaluation.Name = "SelfEvaluation";
            // 
            // AppraiserGoalVerification
            // 
            this.AppraiserGoalVerification.Activities.Add(this.AppraiserVerificationStateInitiation);
            this.AppraiserGoalVerification.Activities.Add(this.edAppraiserVerification);
            this.AppraiserGoalVerification.Name = "AppraiserGoalVerification";
            // 
            // VFS_AppraisalWorkflowInitialState
            // 
            this.VFS_AppraisalWorkflowInitialState.Activities.Add(this.edInitiation);
            this.VFS_AppraisalWorkflowInitialState.Name = "VFS_AppraisalWorkflowInitialState";
            // 
            // VFS_AppraisalWorkflow
            // 
            this.Activities.Add(this.VFS_AppraisalWorkflowInitialState);
            this.Activities.Add(this.AppraiserGoalVerification);
            this.Activities.Add(this.SelfEvaluation);
            this.Activities.Add(this.AppraiserEvaluation);
            this.Activities.Add(this.ReviewerVerification);
            this.Activities.Add(this.HR);
            this.Activities.Add(this.ClosedState);
            this.Activities.Add(this.AppraiseeSignOffOrAppeal);
            this.Activities.Add(this.AppraiseeGoalSetting);
            this.Activities.Add(this.H2InitialGoalSetting);
            this.Activities.Add(this.AppraiserH2GoalVerification);
            this.Activities.Add(this.H2SelfEvaluation);
            this.Activities.Add(this.H2AppraiserEvaluation);
            this.Activities.Add(this.H2ReviewerVerification);
            this.Activities.Add(this.H2AppraiseeSignOffOrAppeal);
            this.Activities.Add(this.H2HR);
            this.Activities.Add(this.H1EmptyState);
            this.Activities.Add(this.H2EmptyState);
            this.Activities.Add(this.H1Deactivation);
            this.CompletedStateName = "ClosedState";
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "VFS_AppraisalWorkflowInitialState";
            this.Name = "VFS_AppraisalWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private TerminateActivity terminateActivity8;

        private IfElseBranchActivity ifElseBranchActivity17;

        private IfElseBranchActivity ifH2HRDeactivation;

        private IfElseActivity ifElseH2HRDeactivation;

        private TerminateActivity terminateActivity7;

        private IfElseBranchActivity ifElseBranchActivity16;

        private IfElseBranchActivity ifH2SignOffDeactivation;

        private IfElseActivity ifElseH2AppraiseeSignOff;

        private IfElseBranchActivity ifElseBranchActivity15;

        private IfElseBranchActivity ifH2RvrDeactivation;

        private IfElseActivity ifElseH2RvrEvalDeactivation;

        private TerminateActivity terminateActivity6;

        private TerminateActivity terminateActivity5;

        private IfElseBranchActivity ifElseBranchActivity13;

        private IfElseBranchActivity ifElseBranchActivity1;

        private IfElseActivity ifElseH2AppraiserEvalDeactivation;

        private TerminateActivity terminateActivity4;

        private IfElseBranchActivity ifElseBranchActivity12;

        private IfElseBranchActivity ifH2EmpttyDeactivation;

        private IfElseActivity ifElseH2EmptyDeactivation;

        private TerminateActivity terminateActivity3;

        private IfElseBranchActivity ifElseBranchActivity11;

        private IfElseBranchActivity ifH2SelfDeactivation;

        private IfElseActivity ifElseH2SelfDeactivation;

        private TerminateActivity terminateActivity2;

        private IfElseBranchActivity ifElseBranchActivity10;

        private IfElseBranchActivity ifH2AppraiserDeactivation;

        private IfElseActivity ifElseH2AppraiserDeactivation;

        private TerminateActivity terminateActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity50;

        private IfElseBranchActivity ifElseBranchActivity9;

        private IfElseBranchActivity ifH2GoalsCompleted;

        private IfElseActivity ifElseH2GoalSetting;

        private SetStateActivity setStateActivity12;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity49;

        private SetStateActivity setStateActivity11;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity48;

        private IfElseBranchActivity ifElseBranchActivity8;

        private IfElseBranchActivity ifH1HRDeactivation;

        private IfElseActivity ifElseH1HRDeactivation;

        private IfElseBranchActivity ifElseBranchActivity7;

        private IfElseBranchActivity ifH1AppraiseeSignOffDeactivation;

        private IfElseActivity ifElseH1AppraiseeSingOffDeactivation;

        private SetStateActivity setStateActivity10;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity47;

        private IfElseBranchActivity ifElseBranchActivity6;

        private IfElseBranchActivity ifRvrEvalDeactivation;

        private IfElseActivity ifElseH1RvrEvalDeactiovation;

        private SetStateActivity setStateToReviewerVerification;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity6;

        private SetStateActivity setStateActivity9;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity46;

        private IfElseBranchActivity ifElseBranchActivity5;

        private IfElseBranchActivity ifH1AppraiserEvalDeactivated;

        private IfElseActivity ifH1AppraiserEvalDeactivation;

        private SetStateActivity setStateActivity8;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity45;

        private IfElseBranchActivity ifElseBranchActivity4;

        private IfElseBranchActivity ifH1EmptyDeactivation;

        private IfElseActivity ifElseH1Empty;

        private SetStateActivity setStateActivity6;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity43;

        private IfElseBranchActivity ifElseBranchActivity3;

        private IfElseBranchActivity ifH1AppraiserDeactivation;

        private IfElseActivity ifElseH1AppraiserDeactivation;

        private SetStateActivity setStateActivity7;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity44;

        private IfElseBranchActivity ifElseBranchActivity2;

        private IfElseBranchActivity ifH1SelfDeactivation;

        private IfElseActivity ifElseH1SelfDeactivation;

        private SetStateActivity setStateToAppraiserEvaluation;

        private IfElseBranchActivity ifH1GoalSettingComplete;

        private IfElseBranchActivity ifH1Deactivated;

        private IfElseActivity ifElseH1AppraiseeDeactivation;

        private SetStateActivity setStateToDeactivation1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity41;

        private SetStateActivity setStateToH2GoalSetting;

        private IfElseBranchActivity ifH2NotStarted;

        private IfElseBranchActivity ifH2Started;

        private IfElseActivity ifElseH2Started;

        private SetStateActivity setStateToH2Start;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity42;

        private CodeActivity caCheckH1orH2;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedH1Deactivation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity35;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForDeactivation;

        private EventDrivenActivity edH1Deactivation;

        private StateInitializationActivity stateIniH1Deactivation;

        private StateActivity H1Deactivation;

        private SetStateActivity setStateActivity5;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity18;

        private SetStateActivity setStateActivity4;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity40;

        private SetStateActivity setStateActivity3;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity39;

        private IfElseBranchActivity ifH2GoalsAmendment;

        private IfElseBranchActivity ifH2TMTTrigger;

        private IfElseActivity ifH2Evaluation;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onH2EmptyTaskChanged;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity33;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2Empty;

        private EventDrivenActivity edH2Empty;

        private StateInitializationActivity stateIniH2Empty;

        private StateActivity H2EmptyState;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity36;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH1Empty;

        private StateInitializationActivity stateIniH1Empty;

        private StateActivity H1EmptyState;

        private SetStateActivity setStateActivity2;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity38;

        private SetStateActivity setStateActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity37;

        private IfElseBranchActivity ifGoalsAmendment;

        private IfElseBranchActivity ifTMTTrigger;

        private IfElseActivity ifH1Evaluation;

        private EventDrivenActivity edH1Empty;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onH1EmptyTaskChanged;

        private SetStateActivity setStateToH1GoalChanges;

        private SetStateActivity setStateH2GoalSetting;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity34;

        private IfElseBranchActivity ifElseH2TMTInitiationNotStarted;

        private IfElseBranchActivity ifElseH2TMTInitiationStarted;

        private IfElseActivity ifElseH2TMTInitiation;

        private CodeActivity caForH2TMTInitiation;

        private SetStateActivity setStateToH2AppraiserRevaluationHR;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity32;

        private SetStateActivity setStateToClosedState;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity31;

        private IfElseBranchActivity isH2CountryHRRequest;

        private IfElseBranchActivity isH2CountryHRClose;

        private IfElseActivity ifElseH2CountryHR;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForH2CountryHR;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity30;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2CountryHR;

        private EventDrivenActivity edH2HR;

        private StateInitializationActivity stateIniH2HR;

        private StateActivity H2HR;

        private SetStateActivity setStateH2HR;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity29;

        private SetStateActivity setStateToH2AppraiserEvaluation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity28;

        private IfElseBranchActivity isH2AppraiseeSignedOff;

        private IfElseBranchActivity isH2AppraiseeAppeal;

        private IfElseActivity ifElseH2AppraiseeAppeal;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForH2AppraiseeSignOff;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity27;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2AppraiseeSignOff;

        private EventDrivenActivity edH2AppraiseeSignOff;

        private StateInitializationActivity stateIniH2AppraiseeSignOff;

        private StateActivity H2AppraiseeSignOffOrAppeal;

        private SetStateActivity setStateToH2AppraiseeSignOffOrAppeal;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity26;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedH2ReviewerVerification;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity25;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2ReviewerVerification;

        private EventDrivenActivity edH2ReviewerVerification;

        private StateInitializationActivity stateIniH2ReviewerVerification;

        private StateActivity H2ReviewerVerification;

        private SetStateActivity setStateH2ReviewerVerification;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity24;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForH2AppraiserEvaluation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity23;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2AppraiserEvaluation;

        private EventDrivenActivity edH2AppraiserVerification;

        private StateInitializationActivity stateIniH2AppraiserVerification;

        private StateActivity H2AppraiserEvaluation;

        private SetStateActivity setStateH2AppraiserEvaluation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity22;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedH2SelfEvaluation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity21;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2SelfEvaluation;

        private EventDrivenActivity edH2SelfEvaluation;

        private StateInitializationActivity stateIniH2SelfEvaluation;

        private StateActivity H2SelfEvaluation;

        private SetStateActivity setStateToH2SelfEvaluation;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity20;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForAppraiserH2GoalVerification;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity19;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForAppraiserH2GoalVerification;

        private EventDrivenActivity edAppraiserH2Verification;

        private StateInitializationActivity H2AppraiserVerificationStateInitiation;

        private StateActivity AppraiserH2GoalVerification;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedH2GoalSetting;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity17;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForH2InitialGoalSetting;

        private EventDrivenActivity edAppraiseeH2InitialGoalSetting;

        private StateInitializationActivity AppraiseeH2InitialGoalSetting;

        private StateActivity H2InitialGoalSetting;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForInitialGoalSetting;

        private SetStateActivity setSateToGoalVerification;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity16;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedGoalSetting;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity15;

        private EventDrivenActivity edAppraiseeGoalSetting;

        private StateInitializationActivity AppraiseeInitialGoalSetting;

        private StateActivity AppraiseeGoalSetting;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity1;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity11;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity10;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity14;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity13;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity2;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity9;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity12;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity8;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity7;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity5;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity4;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistoryListActivity3;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logToHistory1;

        private SetStateActivity setStateToCountryHR;

        private SetStateActivity setStateToAppraiserRevaluation;

        private SetStateActivity setStateToAppraiserRevaluationHR;

        private SetStateActivity setStateToH2InitialGoalSetting;

        private SetStateActivity setStateToSelfEvaluation;

        private IfElseBranchActivity isAppraiseeSignedOff;

        private IfElseBranchActivity isAppraiseeAppeal;

        private IfElseBranchActivity isCountryHRRequest;

        private IfElseBranchActivity isCountryHRClose;

        private IfElseBranchActivity ifElseTMTInitiationNotStarted;

        private IfElseBranchActivity ifElseTMTInitiationStarted;

        private IfElseActivity ifElseAppraiseeAppeal;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForAppraiseeSignOff;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForAppraiseeSignOff;

        private IfElseActivity ifElseCountryHR;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForCountryHR;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForCountryHR;

        private SetStateActivity setStateToAppraiseeSignOff;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedReviewerVerification;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForReviewerVerification;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForAppraiserEvaluation;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForAppraiserEvaluation;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedSelfEvaluation;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForSelfEvaluation;

        private IfElseActivity ifElseTMTInitiation;

        private CodeActivity caForTMTInitiation;

        private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChangedForAppraiserGoalVerification;

        private Microsoft.SharePoint.WorkflowActions.CreateTaskWithContentType createTaskForAppraiserGoalVerification;

        private SetStateActivity setStateToAppraiseeGoalSetting;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;

        private EventDrivenActivity edAppraiseeSignOff;

        private StateInitializationActivity stateIniAppraiseeSignOff;

        private EventDrivenActivity edHR;

        private StateInitializationActivity stateIniHR;

        private EventDrivenActivity edReviewerVerification;

        private StateInitializationActivity stateIniReviewerVerification;

        private EventDrivenActivity edAppraiserEvaluation;

        private StateInitializationActivity stateIniAppraiserEvaluation;

        private EventDrivenActivity edSelfEvaluation;

        private StateInitializationActivity stateIniSelfEvaluation;

        private EventDrivenActivity edAppraiserVerification;

        private StateInitializationActivity AppraiserVerificationStateInitiation;

        private EventDrivenActivity edInitiation;

        private StateActivity AppraiseeSignOffOrAppeal;

        private StateActivity ClosedState;

        private StateActivity HR;

        private StateActivity ReviewerVerification;

        private StateActivity AppraiserEvaluation;

        private StateActivity SelfEvaluation;

        private StateActivity AppraiserGoalVerification;

        private StateActivity VFS_AppraisalWorkflowInitialState;












































































































































































































































































































































    }
}
