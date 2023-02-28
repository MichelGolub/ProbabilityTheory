<template>
<ul class="sidenav" id="slide-out">
    <li class="no-padding">
        <ul class="collapsible collapsible-accordion">
            <li v-for="(theme, themeNumber) in themes" :key="themeNumber">
                <div class="collapsible-header">{{ (themeNumber+1) + ' ' + theme.title }}</div>
                <div class="collapsible-body">
                    <ul>
                        <li v-for="(chapter, chapterNumber) in theme.chapters" :key="chapterNumber"
                            :class="{ active: $route.params['id'] === chapter.id }">
                            <router-link :to="{ name: 'pages', params: { id: chapter.id }}" class="sidenav-close truncate">
                                {{ (themeNumber+1) + '.' + (chapterNumber+1) + ' ' + chapter.title }}
                            </router-link>
                        </li>
                    </ul>
                </div>
            </li>
        </ul>
    </li>
</ul>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: "SidenavMenu",
    computed: {
        ...mapGetters([
            'themes'
        ]),
        languageId() {  return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'getThemes'
        ]),
        loadThemes() {
            let languageId = this.languageId
            if(!languageId) {
                return
            }
            this.getThemes({languageId})
        },
    },
    mounted() {
        $('.collapsible').collapsible()
        $('.sidenav').sidenav()

        this.loadThemes()
    },
    watch: {
        languageId() {
            this.loadThemes()
        }
    }
}
</script>