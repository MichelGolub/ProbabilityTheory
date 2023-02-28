<template>
    <ul id="languagesDropdown" class="dropdown-content">
        <li v-for="(language, index) in languages" :key="index">
            <a @click="setLocale(language)">{{ language.code }}</a>
        </li>
    </ul>
    <ul class="right hide-on-med-and-down">
        <li>
            <a class="dropdown-trigger"
            data-target="languagesDropdown">
                <WebIcon /> {{ $i18n.locale }}
            </a>
        </li>
    </ul>
</template>

<script>
import { mapGetters, mapActions } from "vuex"

import WebIcon from 'icons/Web'

export default {
    name: 'LanguageSwitcher',
    components: {
        WebIcon
    },
    computed: {
        ...mapGetters([
            'languages'
        ])
    },
    methods: {
        ...mapActions([
            'getLanguages'
        ]),
        setLocale(language) {
            this.$i18n.locale = language.code
        }
    },
    mounted() {
        $(".dropdown-trigger").dropdown()

        this.getLanguages()
    }
}

</script>