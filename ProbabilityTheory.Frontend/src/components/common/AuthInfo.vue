<template>
    <li v-if="0&&isLoggedIn"><a @click="login">{{ $t('login') }}</a></li>
    <li v-if="1||isLoggedIn"><a @click="logout"><LogoutIcon/> {{ $t('logout') }}</a></li>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'

import LogoutIcon from 'icons/Logout'

export default {
    name: "AuthInfo",
    components: {
        LogoutIcon
    },
    /*
    computed: {
        ...mapGetters({
            isLoggedIn: 'oidcIsAuthenticated',
            user: 'oidcUser'
        }),
    },
    methods: {
        ...mapActions([
            'signOutOidc',
            'authenticateOidc'
        ]),
        login() {
            this.authenticateOidc()
        },
        logout() {
            this.signOutOidc()
        }
    }
    */
}

</script>


<i18n>
{
    "eng": {
        "logout": "logout"
    },
    "ukr": {
        "logout": "вийти"
    }
}
</i18n>