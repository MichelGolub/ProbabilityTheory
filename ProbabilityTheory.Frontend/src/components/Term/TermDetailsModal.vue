<template>
<div id="termDetailsModal" class="modal">
    <div class="modal-content">
        <div v-show="!editing">
            <h4> 
                {{ term.title }} 
                <EditIcon @click="editing=true" v-if="authorized" style=" cursor: pointer"/>
            </h4>
            <p>{{ term.definition }} </p>
        </div>
        <div v-show="editing">
            <TermForm :term="term" v-on:done="editing=false"/>
        </div>
    </div>
</div>
</template>


<script>
import { mapGetters, mapActions } from 'vuex'

import TermForm from '@/components/Term/TermFormModal.vue'

import EditIcon from 'icons/Pencil'

export default {
    name: 'TermDetailsModal',
    components: {
        TermForm,
        EditIcon
    },
    data() {
        return {
            editing: false
        }
    },
    computed: {
        ...mapGetters([
            'term'
        ]),
        authorized() { return true },
        languageId() { return this.$store.getters.languageIdByCode(this.$i18n.locale) }
    },
    methods: {
        ...mapActions([
            'getTerm',
            'updateTerm'
        ]),
        open(id) {
            this.$store.commit('setTerm', {id})
            this.loadTerm()
        },
        loadTerm() {
            this.editing = false
            let languageId = this.languageId
            let id = this.term.id
            if(!languageId) {
                return
            }
            this.getTerm({id, languageId})
            .then(() => {
                $('#termDetailsModal').modal('open')
            })
        },
    },
    mounted() {
        $('.modal').modal();
    }
}
</script>
