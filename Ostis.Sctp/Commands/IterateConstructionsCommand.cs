using Ostis.Sctp.Arguments;

namespace Ostis.Sctp.Commands
{
#warning Эта команда не реализована, ибо не понятны чаяния Корончика
//    function _unit_test_sctp_iter_constr() {
//    window.sctpClient.iterate_constr(
//        SctpConstrIter(SctpIteratorType.SCTP_ITERATOR_5F_A_A_A_F,
//                      [window.scKeynodes.nrel_system_identifier,
//                       sc_type_arc_common | sc_type_const,
//                       sc_type_link,
//                       sc_type_arc_pos_const_perm,
//                       window.scKeynodes.nrel_main_idtf
//                      ], 
//                      {"x": 2}),
//        SctpConstrIter(SctpIteratorType.SCTP_ITERATOR_3F_A_F,
//                       [window.scKeynodes.lang_ru,
//                        sc_type_arc_pos_const_perm,
//                        "x"
//                       ])
//    ).done(function(results) {
//        console.log(results);
//        //! @todo: make results check
        
//        console.log("x: " + results.get(0, "x"));
//        window.sctpClient.get_link_content(results.get(0, "x")).done(function (res) {
//            console.log("x: " + res);
//        });
//    });
//}
    /// <summary>
    /// Команда: Итерирование конструкций.
    /// </summary>
    public class IterateConstructionsCommand : Command
    {
        #region Параметры команды

        /// <summary>
        /// Шаблон поиска.
        /// </summary>
        public ConstructionTemplate Template
        { get { return template; } }

        private readonly ConstructionTemplate template;

        #endregion
        
        /// <summary>
        /// Инициализирует новую команду.
        /// </summary>
        /// <param name="template">шаблон поиска</param>
		public IterateConstructionsCommand(ConstructionTemplate template)
            : base(CommandCode.IterateConstructions)
        {
            Arguments.Add(this.template = template);
        }
    }
}
