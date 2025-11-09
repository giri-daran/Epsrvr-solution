using EPiServer.Enterprise;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using EpiserverBH.Models.Folder;
using EpiserverBH.Models.Pages;

namespace EpiserverBH.Extensions
{
    public static class Extensions
    {
        private static readonly Lazy<IContentRepository> _contentRepository =
            new Lazy<IContentRepository>(() => ServiceLocator.Current.GetInstance<IContentRepository>());

        private static readonly Lazy<IUrlResolver> _urlResolver =
           new Lazy<IUrlResolver>(() => ServiceLocator.Current.GetInstance<IUrlResolver>());

        private static readonly Lazy<IPermanentLinkMapper> _permanentLinkMapper =
           new Lazy<IPermanentLinkMapper>(() => ServiceLocator.Current.GetInstance<IPermanentLinkMapper>());

      


      
        public static ContentReference GetRelativeStartPage(this IContent content)
        {
            if (content is Home)
            {
                return content.ContentLink;
            }

            var ancestors = _contentRepository.Value.GetAncestors(content.ContentLink);
            var startPage = ancestors.FirstOrDefault(x => x is Home) as Home;
            return startPage == null ? ContentReference.StartPage : startPage.ContentLink;
        }


        public static IEnumerable<T> FindPagesRecursively<T>(this IContentLoader contentLoader, PageReference pageLink) where T : PageData
        {
            foreach (var child in contentLoader.GetChildren<T>(pageLink))
            {
                yield return child;
            }

            foreach (var folder in contentLoader.GetChildren<FolderPage>(pageLink))
            {
                foreach (var nestedChild in contentLoader.FindPagesRecursively<T>(folder.PageLink))
                {
                    yield return nestedChild;
                }
            }
        }

     

        private static void UpdateLanguageBranches(IImportStatus status)
        {
            var languageBranchRepository = ServiceLocator.Current.GetInstance<ILanguageBranchRepository>();

            if (status.ContentLanguages == null)
            {
                return;
            }

            foreach (var languageId in status.ContentLanguages)
            {
                var languageBranch = languageBranchRepository.Load(languageId);

                if (languageBranch == null)
                {
                    languageBranch = new LanguageBranch(languageId);
                    languageBranchRepository.Save(languageBranch);
                }
                else if (!languageBranch.Enabled)
                {
                    languageBranch = languageBranch.CreateWritableClone();
                    languageBranch.Enabled = true;
                    languageBranchRepository.Save(languageBranch);
                }
            }
        }

        private static void RunIndexJob(IScheduledJobExecutor scheduledJobExecutor, IScheduledJobRepository scheduledJobRepository, Guid jobId)
        {
            var job = scheduledJobRepository.Get(jobId);
            if (job == null)
            {
                return;
            }

            scheduledJobExecutor.StartAsync(job, new JobExecutionOptions { Trigger = ScheduledJobTrigger.User });
        }

      
    }
}