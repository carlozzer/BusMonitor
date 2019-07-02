using BusMonitor.BLL.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusMonitor.BLL.Templating
{
    public static class Template
    {
        #region TRY CATCH FINALLY
        
        public static bool
        Try ( 
                    Action TryBlock, 
                    Action<Exception> CatchBlock = null, 
                    Action FinallyBlock = null 
        ) {

            bool ret = false;

            try {

                TryBlock();
                ret = true;

            } catch ( Exception ex) {

                if (CatchBlock != null) {

                    CatchBlock(ex);

                } 

                // excepción siempre deja mensaje en LOG
                //ExceptionHelper.Manage(ex);
                ret = false;
            } finally  {

                if (FinallyBlock != null) {

                    FinallyBlock();

                }

            }

            return ret;
        }

        #endregion

        #region CHRONOMETER

        public static TimeSpan 
        Chronometer ( Action ActionBlock ) {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            ActionBlock();

            watch.Stop();
            return watch.Elapsed;
        }

        public static void 
        Chronometer ( string desc , Action ActionBlock ) {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            ActionBlock();

            watch.Stop();
            //debugger.Message( "'{0}' exec time: {1}".ApplyFormat(desc , watch.Elapsed.ToString() ) );
        }
		 
	    #endregion

        #region SUPER IF

        public static T SuperIf<T>( params ConditionResult<T>[] conditions ) {

            T ret = default(T);

            Template.Try( () => {

               conditions.SafeForEach( item => {

                   if ( item.condition ) {
                       ret = item.result;
                   }

               });

            });

            return ret;
        }

        #endregion

    }

}
