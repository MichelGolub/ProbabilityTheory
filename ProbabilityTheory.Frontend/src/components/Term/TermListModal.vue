<template>
    <div id="termsModal" class="container col6 modal">
        <div class="modal-content">
            <div class="row">
                <div class="input-field">
                    <SearchIcon class="material-icons prefix"/>
                    <input id="search" type="search" v-model="filter.title" @input="filterTerms">
                    <label for="search">{{ $t('search') }}</label>
                </div>
            </div>

            <ul class="collection">
                <li class="collection-item" v-for="term in terms" :key="term.id">
                    <a style="cursor: pointer" @click="returnTermId(term.id)">
                        {{ term.title }}
                    </a>
                </li>
            </ul>

            <div class="row" v-show="!terms.length">
                <h4>{{ $t('noTerms') }}</h4>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'

import SearchIcon from 'icons/Magnify'

export default {
    name: 'TermsView',
    emits: ['idSelected'],
    components: {
        SearchIcon
    },
    data() {
        return {
            filter: {
                title: ''
            },
            terms: []
        }
    },
    computed: {
        ...mapGetters([
            'filterTermsByTitle'
        ]),
        languageId() {  return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'getTerms'
        ]),
        loadTerms() {
            let languageId = this.languageId
            if(!languageId) {
                return
            }
            this.getTerms({languageId})
            .then(() => {
                this.filterTerms()
            })
        },
        filterTerms() {
            let title = this.filter.title
            this.terms = this.filterTermsByTitle(title)
        },
        returnTermId(id) {
            this.$emit('idSelected', id)
            $('#termsModal').modal('close')
        }
    },
    mounted() {
        $('.modal').modal()
        this.loadTerms()
    },
    watch: {
        languageId() {
            this.loadTerms()
        }
    }
}
</script>


<i18n>
{
    "eng": {
        "noTerms": "There is no terms",
        "search": "Search"
    },
    "eng": {
        "noTerms": "Немає термінів",
        "search": "Пошук"
    }
}
</i18n>