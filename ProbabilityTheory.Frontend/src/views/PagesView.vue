<template>
    <div>
        <div class="row">
            <a @click="addPage" v-if="authorized" style="cursor: pointer">
                {{ $t('page.add') }}
            </a>
        </div>

        <div class="row">
            <router-view></router-view>
        </div>

        <div class="divider"></div>
        <div class="row">
            <div class="center-align">
                <ul class="pagination">
                    <li v-for="(page, index) in pages" :key="index"
                    :class="{ active: $route.params['pageId'] === page.id }"
                    class="waves-effect">
                        <router-link :to="{ name: 'page', params: { pageId: page.id } }">
                            {{ index + 1 }}
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>

        <div class="row">
            <h5 v-show="!pages?.length">{{ $t('page.noPages') }}</h5>
        </div>
    </div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

export default {
    name: 'PagesView',
    computed: {
        ...mapGetters([
            'chapter'
        ]),
        pages() { return this.chapter.pages },
        pagesLength() { return this.pages.length },
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) },
        id() { return this.$route.params.id }
    },
    methods: {
        ...mapActions([
            'createPage'
        ]),
        addPage() {
            let chapterId = this.id
            this.createPage({chapterId})
            .then((resp) => {
                this.$router.push({ name: 'page', params: { pageId: resp.data } })
            })
        },
        moveToFirstPageIfExist() {
            if (this.pages.length) {
                let page = this.pages[0]
                let route = this.$router.resolve({
                    name: 'page',
                    params: { pageId: page.id }
                })
                this.$router.push(route)
            }
        }
    },
    mounted() {
        this.moveToFirstPageIfExist()
    },
    watch: {
        pagesLength() {
            this.moveToFirstPageIfExist()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "page": {
            "add": "Add new page",
            "noPages": "There is no pages"
        }
    },
    "ukr": {
        "page": {
            "add": "Створити нову сторінку",
            "noPages": "Немає сторінок"
        }
    }
}
</i18n>