<template>
<div class="div">
    <button @click="editor.chain().focus().toggleBold().run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('bold') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><BoldIcon/></span>
    </button>
    <button @click="editor.chain().focus().toggleItalic().run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('italic') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><ItalicIcon/></span>
    </button>
    <button @click="editor.chain().focus().toggleStrike().run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('strike') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><StrikeIcon/></span>
    </button> |

    <button @click="editor.chain().focus().toggleBlockquote().run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('blockquote') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><TabIcon/></span>
    </button> |

    <button @click="setLink"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('link') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><LinkPlusIcon/></span>
    </button>
    <button @click="editor.chain().focus().unsetLink().run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive('link') ? 'blue-grey darken-1' : 'cyan darken-1']">
        <span><LinkOffIcon/></span>
    </button> |

    <button @click="editor.chain().focus().setTextAlign('left').run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive({ textAlign: 'left' }) ? 'blue-grey darken-1' : 'cyan darken-1' ]">
        <span><TextAlignLeftIcon/></span>
    </button>
    <button @click="editor.chain().focus().setTextAlign('center').run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive({ textAlign: 'center' }) ? 'blue-grey darken-1' : 'cyan darken-1' ]">
        <span><TextAlignCenterIcon/></span>
    </button>
    <button @click="editor.chain().focus().setTextAlign('right').run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive({ textAlign: 'right' }) ? 'blue-grey darken-1' : 'cyan darken-1' ]">
        <span><TextAlignRightIcon/></span>
    </button>
    <button @click="editor.chain().focus().setTextAlign('justify').run()"
    class="waves-effect waves-light btn-small btn-editor" :class="[ editor?.isActive({ textAlign: 'justify' }) ? 'blue-grey darken-1' : 'cyan darken-1' ]">
        <span><TextAlignJustifyIcon/></span>
    </button> |

    <button @click="openTermsModal"
    class="waves-effect waves-light btn-small btn-editor cyan darken-1">
        <span>T</span>
    </button> 
</div>

<TermsModal ref="terms" v-on:idSelected="setTermLink"/>
</template>

<script>
import BoldIcon from 'icons/FormatBold'
import ItalicIcon from 'icons/FormatItalic'
import StrikeIcon from 'icons/FormatStrikethrough'
import TabIcon from 'icons/FormatHorizontalAlignRight'
import LinkPlusIcon from 'icons/LinkPlus'
import LinkOffIcon from 'icons/LinkOff'
import TextAlignCenterIcon from 'icons/FormatAlignCenter'
import TextAlignLeftIcon from 'icons/FormatAlignLeft'
import TextAlignRightIcon from 'icons/FormatAlignRight'
import TextAlignJustifyIcon from 'icons/FormatAlignJustify'

import TermsModal from '@/components/Term/TermListModal.vue'

export default {
    name: 'EditorMenu',
    props: ['editor'],
    components: {
        TermsModal,
        BoldIcon, ItalicIcon, StrikeIcon,
        TabIcon,
        LinkPlusIcon, LinkOffIcon,
        TextAlignCenterIcon, TextAlignLeftIcon, TextAlignRightIcon, TextAlignJustifyIcon
    },
    methods: {
        setLink() {
            const previousUrl = this.editor.getAttributes('link').href
            const url = window.prompt('URL', previousUrl)
            if (url === null) {
                return
            }
            if (url === '') {
                this.editor
                    .chain()
                    .focus()
                    .extendMarkRange('link')
                    .unsetLink()
                    .run()
                return
            }
            this.editor
                .chain()
                .focus()
                .extendMarkRange('link')
                .setLink({ href: url })
                .run()
        },
        openTermsModal() {
            $('#termsModal').modal('open')
        },
        setTermLink(id) {
            let props = this.$router.resolve({ 
                name: 'term',
                params: { id },
            })
            this.editor
                .chain()
                .focus()
                .extendMarkRange('link')
                .setLink({ href: props.href })
                .run()
        }

    }
}
</script>

<style>
.btn-editor {
    margin: 2px
}
</style>