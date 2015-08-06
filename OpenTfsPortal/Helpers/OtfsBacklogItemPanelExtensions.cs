using Otfs.ProjectManagement.Core.Domain.Model;
using Otfs.ProjectManagement.Core.Domain.Model.BacklogItem;
using System.Web.Mvc;

namespace OpenTfsPortal.Helpers
{
    public static class OtfsBacklogItemPanelExtensions
    {
        public static MvcHtmlString BacklogItemSuccessPanel(this HtmlHelper helper, OtfsBacklogItem backlogItem)
        {
            return Build("success", backlogItem,true);
        }
        
        public static MvcHtmlString BacklogItemDeployPanel(this HtmlHelper helper, OtfsBacklogItem backlogItem)
        {
            return Build("success", backlogItem);
        }

        public static MvcHtmlString BacklogItemInfoPanel(this HtmlHelper helper, OtfsBacklogItem backlogItem)
        {
            return backlogItem.Rejeitado() ? Build("danger", backlogItem) : Build("info", backlogItem);
        }

        public static MvcHtmlString BacklogItemDangerPanel(this HtmlHelper helper, OtfsBacklogItem backlogItem)
        {
            return Build("danger", backlogItem);
        }

        public static MvcHtmlString BacklogItemWarningPanel(this HtmlHelper helper, OtfsBacklogItem backlogItem)
        {
            return Build("warning", backlogItem, true);
        }

        private static MvcHtmlString Build(string tipo, OtfsBacklogItem backlogItem, bool showButtons = false)
        {
            var buttons = showButtons ? 
                "<h4 class='pull-right'>" +
                "   <a href='#' class='btn btn-link' data-toggle='modal' data-target='#ModalReprovacao'>" +
                "      <span class='glyphicon glyphicon-ban-circle'></span>" +
                "   </a>" +
                "   <a href='#' class='btn btn-link' data-toggle='modal' data-target='#ModalAprovacao'>" +
                "      <span class='glyphicon glyphicon-saved'></span>" +
                "   </a>" +
                "</h4>"
                : string.Empty;

            var basehtml = "<div class='panel panel-" + tipo + "'>" +
            "   <div class='panel-heading'>" +
            "      <h3 class='panel-title'>" +
            "         <span class='glyphicon glyphicon-tag'></span>" +
            "         {0}" +
            "         &nbsp;" +
            "         <span class='glyphicon glyphicon-user'></span>" +
            "         {1} {3}" +
            "      </h3>" +
            "   </div>" +
            "   <div class='panel-body'>" +
            "      {2} " + buttons +
            "   </div>" +
            "</div>";

            var html = string.Format(basehtml, backlogItem.BacklogItemId.Id, backlogItem.AssignedTo, backlogItem.Title, backlogItem.DataAtualizacao.ToString("dd/MM/yyyy"));
            return new MvcHtmlString(html);
        }
    }
}