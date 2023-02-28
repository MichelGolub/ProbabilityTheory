<template>
    <div id="pageDeleteModal" class="modal">
        <div class="modal-content">
            <h4>{{ $t('message.warning') }}</h4>
            <p>{{ $t('message.delete') }}</p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.cancel') }}
            </a>
            <button @click="removePage" class="modal-close waves-effect waves-green btn-flat">
                {{ $t('button.confirm') }}
            </button>
        </div>
    </div>
</template>


<script>
import { mapActions } from 'vuex'

export default {
    name: "PageDeleteModal",
    methods: {
        ...mapActions([
            'deletePage'
        ]),
        open(id) {
            this.$store.commit('setPageIdToDelete', id)
            $('#pageDeleteModal').modal('open')
        },
        removePage() {
            let id = this.$store.getters['pageIdToDelete']
            this.deletePage({id})
            .then(() => {
                $('#pageDeleteModal').modal('close')
            })
        }
    },
    mounted() {
        $('.modal').modal()
    }
}
</script>