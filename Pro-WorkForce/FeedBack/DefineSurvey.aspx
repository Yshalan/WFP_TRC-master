<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="DefineSurvey.aspx.vb"
    Inherits="FeedBack_DefineSurvey" Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function CheckAllQuestions(id) {
            var masterTable = $find("<%= dgrdQuestions.ClientID%>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chk").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chk").checked = false; // for unchecking the checkboxes
                }
            }
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Define Survey" runat="server" />
            <cc1:TabContainer ID="tcSurvey" runat="server" ActiveTabIndex="0" Width="100%"
                CssClass="Tab" meta:resourcekey="tcSurveyResource1">
                <cc1:TabPanel ID="tpSurvey" runat="server" HeaderText="Survey Defination" ToolTip="Survey" meta:resourcekey="tpSurveyResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSurveyName" runat="server" Text="Survey Name" meta:resourcekey="lblSurveyNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSurveyName" runat="server" meta:resourcekey="txtSurveyNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSurveyName" runat="server" ControlToValidate="txtSurveyName"
                                    Display="None" ErrorMessage="Please Enter Survey Name"
                                    ValidationGroup="grpSave" meta:resourcekey="rfvSurveyNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSurveyName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSurveyName" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label Text="Survey Arabic Name" ID="lblSurveyArabicName" runat="server" meta:resourcekey="lblSurveyArabicNameResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSurveyArabicName" runat="server" meta:resourcekey="txtSurveyArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSurveyArabicName" runat="server" ControlToValidate="txtSurveyArabicName"
                                    Display="None" ErrorMessage="Please Enter Survey Arabic Name"
                                    ValidationGroup="grpSave" meta:resourcekey="rfvSurveyArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSurveyArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSurveyArabicName" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label Text="Survey Language" ID="lblSurveyLanguage" runat="server" meta:resourcekey="lblSurveyLanguageResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlSurveyLanguage" runat="server" meta:resourcekey="ddlSurveyLanguageResource1">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Arabic & English" Selected="True" Value="0" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                                        <telerik:RadComboBoxItem Text="English" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                        <telerik:RadComboBoxItem Text="Arabic" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:CheckBox ID="chkIsWeightage" Text="Weightage Required" runat="server" meta:resourcekey="chkIsWeightageResource1" />
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabSurvey" runat="server" HeaderText="Survey Questions" ToolTip="Survey" meta:resourcekey="TabSurveyResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label Text="Survey Question" ID="lblSurveyQuestion" runat="server" meta:resourcekey="lblSurveyQuestionResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSurveyQuestion" TextMode="MultiLine" runat="server" meta:resourcekey="txtSurveyQuestionResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSurveyQuestion" runat="server" ControlToValidate="txtSurveyQuestion"
                                    Display="None" ErrorMessage="Please Enter Survey Questions"
                                    ValidationGroup="grpAdd" meta:resourcekey="rfvSurveyQuestionResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSurveyQuestion" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSurveyQuestion" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label Text="Survey Question Arabic" ID="lblSurveyQuestionAr" runat="server" meta:resourcekey="lblSurveyQuestionArResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSurveyQuestionAr" TextMode="MultiLine" runat="server" meta:resourcekey="txtSurveyQuestionArResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSurveyQuestionAr" runat="server" ControlToValidate="txtSurveyQuestionAr"
                                    Display="None" ErrorMessage="Please Enter Survey Arabic Questions"
                                    ValidationGroup="grpAdd" meta:resourcekey="rfvSurveyQuestionArResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceSurveyQuestionAr" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvSurveyQuestionAr" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label Text="Question Type" ID="lblQuestionType" runat="server" meta:resourcekey="lblQuestionTypeResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlQuestionType" runat="server" AutoPostBack="True" Width="500px" meta:resourcekey="ddlQuestionTypeResource1">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="--Please Select--" Selected="True" Value="0" runat="server" meta:resourcekey="RadComboBoxItemResource4" />
                                        <telerik:RadComboBoxItem Text="Check Boxes" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                        <telerik:RadComboBoxItem Text="Radio Button" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource6" />
                                        <telerik:RadComboBoxItem Text="Smiles" Value="3" runat="server" meta:resourcekey="RadComboBoxItemResource7" />
                                        <telerik:RadComboBoxItem Text="Rating" Value="4" runat="server" meta:resourcekey="RadComboBoxItemResource8" />
                                        <telerik:RadComboBoxItem Text="Text Input" Value="5" runat="server" meta:resourcekey="RadComboBoxItemResource9" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="pnlMultipleAnswers" runat="server" Visible="False" meta:resourcekey="pnlMultipleAnswersResource1">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label Text="Enter Possible Choices" ID="lblAnswers" runat="server" meta:resourcekey="lblAnswersResource1" />
                                </div>
                                <div>
                                    <asp:DataList ID="dlAnswers" runat="server" RepeatColumns="1" meta:resourcekey="dlAnswersResource1">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfanswerid" runat="server" />
                                            <div>
                                                <div class="col-md-6" style="width: 45% !important">
                                                    <telerik:RadTextBox ID="txtAnswer" runat="server" TextMode="MultiLine" Height="100px"
                                                        EmptyMessage="Enter Answer" LabelWidth="64px" Width="160px" LabelCssClass="" Resize="None" meta:resourcekey="txtAnswerResource1">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </telerik:RadTextBox>

                                                </div>
                                                <div class="col-md-6" style="width: 45% !important">
                                                    <telerik:RadTextBox ID="txtAnswerArbic" runat="server" TextMode="MultiLine" Height="100px"
                                                        EmptyMessage="Enter Arabic Answer" LabelWidth="64px" Width="160px" LabelCssClass="" Resize="None" meta:resourcekey="txtAnswerArbicResource1">
                                                        <EmptyMessageStyle Resize="None" />
                                                        <ReadOnlyStyle Resize="None" />
                                                        <FocusedStyle Resize="None" />
                                                        <DisabledStyle Resize="None" />
                                                        <InvalidStyle Resize="None" />
                                                        <HoveredStyle Resize="None" />
                                                        <EnabledStyle Resize="None" />
                                                    </telerik:RadTextBox>
                                                </div>
                                                <div class="col-md-2" style="line-height: 30px; padding-top: 8px;">
                                                    <asp:ImageButton ID="btnadd" ImageUrl="~/images/plus.png" runat="server" meta:resourcekey="btnaddResource1" />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlSmiley" runat="server" Visible="False" meta:resourcekey="pnlSmileyResource1">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="ddlSmilies" runat="server" Width="500px" CheckBoxes="True" meta:resourcekey="ddlSmiliesResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="--Please Select--" Selected="True" Value="0" runat="server" meta:resourcekey="RadComboBoxItemResource10" />
                                            <telerik:RadComboBoxItem Text="Happy" Value="Happy" ImageUrl="~/assets/img/emoji/happy.png" runat="server" meta:resourcekey="RadComboBoxItemResource11" />
                                            <telerik:RadComboBoxItem Text="straightface" Value="straightface" ImageUrl="~/assets/img/emoji/straightface.png" runat="server" meta:resourcekey="RadComboBoxItemResource12" />
                                            <telerik:RadComboBoxItem Text="sad" Value="sad" ImageUrl="~/assets/img/emoji/sad.png" runat="server" meta:resourcekey="RadComboBoxItemResource13" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlRating" runat="server" Visible="False" meta:resourcekey="pnlRatingResource1">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <asp:Image ID="star1" runat="server" ImageUrl="~/assets/img/ratingstar.png" meta:resourcekey="star1Resource1" />
                                    <asp:Image ID="star2" runat="server" ImageUrl="~/assets/img/ratingstar.png" meta:resourcekey="star2Resource1" />
                                    <asp:Image ID="star3" runat="server" ImageUrl="~/assets/img/ratingstar.png" meta:resourcekey="star3Resource1" />
                                    <asp:Image ID="star4" runat="server" ImageUrl="~/assets/img/ratingstar.png" meta:resourcekey="star4Resource1" />
                                    <asp:Image ID="star5" runat="server" ImageUrl="~/assets/img/ratingstar.png" meta:resourcekey="star5Resource1" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlTextBox" runat="server" Visible="False" meta:resourcekey="pnlTextBoxResource1">
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAnswerTextBox" TextMode="MultiLine" Enabled="False" runat="server" meta:resourcekey="txtAnswerTextBoxResource1"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAddQuestions" runat="server" Text="Add" ValidationGroup="grpAdd" meta:resourcekey="btnAddQuestionsResource1" />
                                <asp:Button ID="btnClearQuestion" runat="server" Text="Clear" meta:resourcekey="btnClearQuestionResource1" />
                                <asp:Button ID="btnDeleteQuestions" runat="server" Text="Delete" meta:resourcekey="btnDeleteQuestionsResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                                <div class="filterDiv">
                                    <telerik:RadFilter runat="server" ID="RadFilter2" Skin="Hay" FilterContainerID="dgrdQuestions"
                                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter2Resource1">
                                        <ContextMenu FeatureGroupID="rfContextMenu">
                                        </ContextMenu>
                                    </telerik:RadFilter>
                                </div>
                                <telerik:RadGrid runat="server" ID="dgrdQuestions" AutoGenerateColumns="False" PageSize="15"
                                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdQuestionsResource1">
                                    <ClientSettings ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="QuestionId,QuestionType">
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn1 column"
                                                UniqueName="Delete" meta:resourcekey="GridTemplateColumnResource1">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" Text="&amp;nbsp" onclick="CheckAllQuestions(this);" meta:resourcekey="chkRowResource1" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="QuestionId" Display="False" FilterControlAltText="Filter QuestionId column" AllowFiltering="False"
                                                HeaderText="QuestionId" UniqueName="QuestionId" meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="QuestionEnText" HeaderText="Question English Text" FilterControlAltText="Filter QuestionEnText column"
                                                UniqueName="QuestionEnText" meta:resourcekey="GridBoundColumnResource5" />
                                            <telerik:GridBoundColumn DataField="QuestionArText" HeaderText="Question Arabic Text" FilterControlAltText="Filter QuestionArText column"
                                                UniqueName="QuestionArText" meta:resourcekey="GridBoundColumnResource6" />
                                        </Columns>
                                        <DetailTables>
                                            <telerik:GridTableView runat="server" Caption="Answer Details" DataMember="AnswerDetails" AllowFilteringByColumn="False"
                                                CellSpacing="0" GridLines="None" Width="100%" meta:resourcekey="GridTableViewResource1">
                                                <ParentTableRelation>
                                                    <telerik:GridRelationFields DetailKeyField="FK_QuestionId" MasterKeyField="QuestionId" />
                                                </ParentTableRelation>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="AnswerId" Display="False" AllowFiltering="False"
                                                        HeaderText="AnswerId" UniqueName="AnswerId" FilterControlAltText="Filter AnswerId column" meta:resourcekey="GridBoundColumnResource1" />
                                                    <telerik:GridBoundColumn DataField="AnswerTextEn" HeaderText="Answer English Text" AllowFiltering="False"
                                                        UniqueName="AnswerTextEn" FilterControlAltText="Filter AnswerTextEn column" meta:resourcekey="GridBoundColumnResource2" />
                                                    <telerik:GridBoundColumn DataField="AnswerTextAr" HeaderText="Answer Arabic Text" AllowFiltering="False"
                                                        UniqueName="AnswerTextAr" FilterControlAltText="Filter AnswerTextAr column" meta:resourcekey="GridBoundColumnResource3" />
                                                </Columns>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar2" Skin="Hay" OnButtonClick="RadToolBar2_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar2Resource1">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                        ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSurvey"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                            <ContextMenu FeatureGroupID="rfContextMenu">
                            </ContextMenu>
                        </telerik:RadFilter>
                    </div>
                    <telerik:RadGrid runat="server" ID="dgrdSurvey" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdSurveyResource1">
                        <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="SurveyId">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource2" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn DataField="SurveyId" Display="False" FilterControlAltText="Filter SurveyId column" AllowFiltering="False"
                                    HeaderText="SurveyId" UniqueName="SurveyId" meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn DataField="SurveyName" HeaderText="Survey Name" FilterControlAltText="Filter SurveyName column"
                                    UniqueName="SurveyName" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn DataField="SurveyArabicName" HeaderText="Survey Arabic Name" FilterControlAltText="Filter SurveyArabicName column"
                                    UniqueName="SurveyArabicName" meta:resourcekey="GridBoundColumnResource9" />
                                <telerik:GridBoundColumn DataField="SurveyLanguage" HeaderText="Survey Language" FilterControlAltText="Filter SurveyLanguage column"
                                    UniqueName="SurveyLanguage" meta:resourcekey="GridBoundColumnResource10" />
                                <telerik:GridBoundColumn DataField="HasWeightage" HeaderText="Has Weightage" FilterControlAltText="Filter HasWeightage column"
                                    UniqueName="HasWeightage" meta:resourcekey="GridBoundColumnResource11" />
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                            EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

