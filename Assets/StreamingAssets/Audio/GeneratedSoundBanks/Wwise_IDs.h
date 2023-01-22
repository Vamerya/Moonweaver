/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID MAINMUSIC = 3479155863U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMEPLAYMUSICSTATE
        {
            static const AkUniqueID GROUP = 2854737089U;

            namespace STATE
            {
                static const AkUniqueID BOSS = 1560169506U;
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID EXPLORING = 1823678183U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMEPLAYMUSICSTATE

        namespace MENUSTATE
        {
            static const AkUniqueID GROUP = 1548586727U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PAUSED = 319258907U;
                static const AkUniqueID UNPAUSED = 1365518790U;
            } // namespace STATE
        } // namespace MENUSTATE

        namespace PLAYERHEALTHSTATE
        {
            static const AkUniqueID GROUP = 2698781627U;

            namespace STATE
            {
                static const AkUniqueID HEALTHY = 2874639328U;
                static const AkUniqueID LOWLIFE = 19277431U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERHEALTHSTATE

    } // namespace STATES

    namespace TRIGGERS
    {
        static const AkUniqueID ONHEALTHYHP = 1940819977U;
        static const AkUniqueID ONLOWHP = 1242301544U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
