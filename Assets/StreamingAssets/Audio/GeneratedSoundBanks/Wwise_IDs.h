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
        static const AkUniqueID ASTERHURT = 3991398673U;
        static const AkUniqueID FOOTSTEPS = 2385628198U;
        static const AkUniqueID HEARTBEAT = 2179486487U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SWORDSWOOSH = 2927843501U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMEPLAYMUSICSTATE
        {
            static const AkUniqueID GROUP = 2854737089U;

            namespace STATE
            {
                static const AkUniqueID EGGBERT = 1367938641U;
                static const AkUniqueID EXPLORING = 1823678183U;
                static const AkUniqueID MAINMENU = 3604647259U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID VERONICA = 2133809136U;
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

    namespace SWITCHES
    {
        namespace ASTERHURT
        {
            static const AkUniqueID GROUP = 3991398673U;

            namespace SWITCH
            {
                static const AkUniqueID ASTERALIVE = 3983317581U;
            } // namespace SWITCH
        } // namespace ASTERHURT

        namespace FOOTSTEPS
        {
            static const AkUniqueID GROUP = 2385628198U;

            namespace SWITCH
            {
                static const AkUniqueID CONCRETE = 841620460U;
                static const AkUniqueID GRAVEL = 2185786256U;
            } // namespace SWITCH
        } // namespace FOOTSTEPS

        namespace SWORDSWOOSH
        {
            static const AkUniqueID GROUP = 2927843501U;

            namespace SWITCH
            {
                static const AkUniqueID DEFAULT = 782826392U;
            } // namespace SWITCH
        } // namespace SWORDSWOOSH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MASTERVOLUME = 2918011349U;
        static const AkUniqueID MUSICVOLUME = 2346531308U;
        static const AkUniqueID SFXVOLUME = 988953028U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID PLAYERLOWLIFE = 2116796762U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX___ENVIRONMENTAL = 1359681556U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
