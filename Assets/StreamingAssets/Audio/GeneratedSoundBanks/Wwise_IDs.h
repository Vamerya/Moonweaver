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
        static const AkUniqueID VERONICABOSSMUSIC = 212041160U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MUSIC_GAMEPLAY_STATE
        {
            static const AkUniqueID GROUP = 585797371U;

            namespace STATE
            {
                static const AkUniqueID BOSS = 1560169506U;
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID EXPLORING = 1823678183U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC_GAMEPLAY_STATE

        namespace PLAYERCOMBAT
        {
            static const AkUniqueID GROUP = 1634810678U;

            namespace STATE
            {
                static const AkUniqueID INBOSSCOMBAT = 687614019U;
                static const AkUniqueID INCOMBAT = 3373579172U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OUTOFCOMBAT = 2773031252U;
            } // namespace STATE
        } // namespace PLAYERCOMBAT

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
        static const AkUniqueID MUSIC = 3991942870U;
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
