using System.Linq;
using System.Collections.Generic;
using System;
using Otfs.ProjectManagement.Core.Domain.Model.Project;
using Otfs.ProjectManagement.Core.Domain.Model.Iteration;

namespace Otfs.ProjectManagement.Core.Domain.Model.BacklogItem
{
    public partial class OtfsBacklogItem
    {
        public OtfsBacklogItemId BacklogItemId { get; private set; }
        public OtfsProjectId ProjectId { get; private set; }
        public OtfsIteration Iteration { get; private set; }
        public OtfsBacklogItemState State { get; private set; }

        public string AssignedTo { get; private set; }
        public string Title { get; private set; }
        public string Tag { get; private set; }
        public string History { get; private set; }
        public string[] Tags { get; private set; }
        public DateTime DataAtualizacao { get; private set; }


        public OtfsBacklogItem(OtfsProjectId projectId, OtfsBacklogItemId backlogItemId, string title, string assignedTo, DateTime dataAtualizacao, OtfsBacklogItemState state = OtfsBacklogItemState.New  )
        {
            this.BacklogItemId = backlogItemId;
            this.AssignedTo = assignedTo;
            this.ProjectId = projectId;
            this.Title = title;
            this.State = state;
            this.DataAtualizacao = dataAtualizacao;
        }

        public void Comitar(OtfsIteration iteration)
        {
            this.Iteration = iteration;
        }

        public bool Rejeitado()
        {
            return this.Tags.ToList().Contains("Rejeitado");
        }

        public bool Publicado()
        {
            return this.Tags.ToList().Contains("Publicado");
        }
        
        public bool Aprovado()
        {
            return this.Tags.ToList().Contains("Aprovado");
        }

        internal void AlterarStatusParaDone()
        {
            this.State = OtfsBacklogItemState.Done;
        }

        internal void AlterarStatusParaCommitted()
        {
            this.State = OtfsBacklogItemState.Committed;
        }

        internal void AlterarStatusParaApproved()
        {
            this.State = OtfsBacklogItemState.Approved;
        }

        internal void MarcarComoRejeitado(string cause)
        {
            this.State = OtfsBacklogItemState.New;
            this.History = cause;
            this.Tag = "Rejeitado";
        }
        
        internal void MarcarComoAprovado(string cause)
        {
            this.History = cause;
            this.Tag = "Aprovado";
        }
        
        internal void MarcarComoPublicado(string cause)
        {
            this.History = cause;
            this.Tag = "Publicado";
        }

        public void AdicionarTags(string tags)
        {
            this.Tags = tags.Split(';');
        }
    }
}
