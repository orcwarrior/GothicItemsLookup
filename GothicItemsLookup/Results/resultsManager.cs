using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GothicItemsLookup.Results
{
    // Quite big word 'Maganer' for static class
    // which don't do more than simply jobs
    // on passed Results, but it's still nice to
    // have some separation btwn View and Model
    static class resultsManager
    {
        static public void deleteResultsFiles(ListBox.SelectedObjectCollection inResults)
        {
            foreach (searchResult res in inResults)
            {
                try
                {
                    System.IO.File.Delete(res.src.file);
                    inResults.Remove(res);
                }
                finally { }
            }
        }

        static public void changeResultInstances(ListBox.SelectedObjectCollection inResults, string newID)
        {
            InstanceReplaceWorker repWorker = new InstanceReplaceWorker();
            foreach (scriptSearchResult res in inResults)
            {
                repWorker.addReplaceJob(res.src.file,
                    new InstanceReplaceJob(res.src.line,
                                           res.instance,
                                           newID
                ));
                // later we have no acess so change ID to new one now
                // (so we be able to do multiple IDs changes even if info in form
                // isn't updated)
                res.instance = newID;
            }
            repWorker.doJobs();
        }
    }
}
