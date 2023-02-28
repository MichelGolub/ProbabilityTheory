<template>
    <div id="termDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removeTerm" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "TermDeleteModal",
    emits: ['done'],
    methods: {
        ...mapActions([
            'deleteTerm'
        ]),
        open(id) {
            this.$store.commit('setTerm', {id})
            $('#termDeleteModal').modal('open')
        },
        removeTerm() {
            let id = this.$store.getters['term'].id
            this.deleteTerm({id})
            .then(() => {
                this.$emit('done')
                $('#termDeleteModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>
